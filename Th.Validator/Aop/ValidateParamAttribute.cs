using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ArxOne.MrAdvice.Advice;
using Th.Validator.Constraints;

namespace Th.Validator.Aop
{
    /// <summary>
    /// 参数验证
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ValidateParamAttribute : Attribute, IMethodAdvice
    {
        public static ConcurrentDictionary<PropertyInfo, List<Attribute>> IsChkParamAttrDic = new ConcurrentDictionary<PropertyInfo, List<Attribute>>();

        /// <summary>
        /// 验证类型
        /// </summary>
        private ValidType _type { get; set; }

        /// <summary>
        /// 要验证的参数，不传默认为验证当前方法入参的所有参数
        /// 格式：参数名称1-参数名称2(用-将参数名称隔开)
        /// 例：Regist(RegistModel1 reg1,RegistModel2 reg2)，若需要同时校验reg1，reg2两个参数的值，则传入：reg1-reg2;
        /// </summary>
        private string _paramNames { get; }

        /// <summary>
        /// 组名，仅仅校验参数的特性中和当前组名一致的特性
        /// </summary>
        private string _group { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <param name="paramName">要验证的参数，不传默认为验证当前方法入参的所有参数</param>
        public ValidateParamAttribute(string paramName = "", string group = "", ValidType type = ValidType.Fast)
        {
            _type = type;
            _paramNames = paramName;
            _group = group;
        }

        /// <summary>
        /// Implements advice logic.
        /// Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The method advice context.</param>
        public void Advise(MethodAdviceContext context)
        {
            try
            {
                var allType = GetChkParamType(context);
                if (allType != null && allType.Any())
                {
                    foreach (var paramInfo in allType)
                    {
                        var chkRes = ChkAllProp(paramInfo.Key.GetProperties().ToList(), paramInfo.Value);
                        if (!string.IsNullOrWhiteSpace(chkRes))
                        {
                            // 参数不符合要求
                            if (context.HasReturnValue)
                            {
                                // 如果有返回值，进一步判断返回值类型是否指定类型，若是指定类型，则将错误信息放到指定位置。
                                var methodInfo = context.TargetMethod as MethodInfo;
                                if (methodInfo == null)
                                {
                                    throw new ConstraintViolationException("参数校验失败补充返回值，未找到目标方法");
                                }
                                context.ReturnValue = context.IsTargetMethodAsync
                                    ? AsyncDefaultForType(methodInfo.ReturnType, chkRes)
                                    : DefaultForType(methodInfo.ReturnType, chkRes);
                                return;
                            }
                            throw new ConstraintViolationException(chkRes);
                        }
                    }
                }

                context.Proceed();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 获取要校验参数的类型
        /// </summary>
        /// <param name="context">当前函数内容上下文</param>
        /// <returns>类型集合</returns>
        private Dictionary<Type, object> GetChkParamType(MethodAdviceContext context)
        {
            Dictionary<Type, object> res = new Dictionary<Type, object>();
            List<string> paramNames = GetChkParamNames();

            ParameterInfo[] parameters = context.TargetMethod.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (paramNames == null || paramNames.Contains(parameters[i].Name))
                {
                    res.Add(parameters[i].ParameterType, context.Arguments[i]);
                }
            }

            return res;
        }

        /// <summary>
        /// 获取要校验的参数名称集合
        /// </summary>
        /// <returns>参数名称集合</returns>
        private List<string> GetChkParamNames()
        {
            if (string.IsNullOrWhiteSpace(_paramNames))
            {
                return null;
            }
            List<string> paramNames = _paramNames.Split('-').ToList();
            if (paramNames == null || !paramNames.Any())
            {
                return null;
            }
            return paramNames;
        }

        /// <summary>
        /// 检查所有的参数
        /// </summary>
        /// <param name="props">属性集合</param>
        /// <param name="arg">参数值</param>
        /// <returns>错误原因</returns>
        private string ChkAllProp(List<PropertyInfo> props, object arg)
        {
            if (props == null || !props.Any())
            {
                return null;
            }

            foreach (PropertyInfo prop in props)
            {
                var val = prop.GetValue(arg, null);
                Type elementType = prop.PropertyType;
                // 判断当前元素
                var errMsg = SinglePropChk(prop, val);
                if (!string.IsNullOrWhiteSpace(errMsg))
                {
                    return errMsg;
                }

                if (prop.ShouldChkInnerParam())
                {
                    if (val == null)
                    {
                        continue;
                    }

                    // 有级联检查特性，进一步判断是否list集合或者自定义对象
                    if (prop.PropertyType.IsListType())
                    {
                        elementType = prop.PropertyType.GetElementType();
                        if (elementType == null)
                        {
                            var elementTypes = prop.PropertyType.GetGenericArguments();
                            if (elementTypes != null && elementTypes.Any())
                            {
                                elementType = elementTypes.First();
                            }
                        }
                        if (elementType == null)
                        {
                            return $"无法识别{prop.PropertyType.FullName}的子元素类型";
                        }

                        //  循环判断每个子元素
                        foreach (object arrVal in (IList)val)
                        {
                            //errMsg = elementType.IsNeedRecursionChk()
                            //            ? ChkAllProp(elementType.GetProperties().ToList(), arrVal)
                            //            : SinglePropChk(prop, arrVal);
                            errMsg = elementType.IsNeedRecursionChk()
                                ? ChkAllProp(elementType.GetProperties().ToList(), arrVal)
                                : string.Empty;
                            if (!string.IsNullOrWhiteSpace(errMsg))
                            {
                                return errMsg;
                            }
                        }
                    }
                    if (elementType.IsNeedRecursionChk())
                    {
                        errMsg = ChkAllProp(elementType.GetProperties().ToList(), val);
                        if (!string.IsNullOrWhiteSpace(errMsg))
                        {
                            return errMsg;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 参数检查
        /// </summary>
        /// <param name="prop">参数类型</param>
        /// <param name="arg">参数值</param>
        /// <returns>检查结果，true=校验通过</returns>
        private string SinglePropChk(PropertyInfo prop, object arg)
        {
            if (IsChkParamAttrDic.ContainsKey(prop))
            {
                return ChkAllAttr(IsChkParamAttrDic[prop], arg, prop);
            }
            if (prop.CustomAttributes.Any())
            {
                List<Attribute> attributes = new List<Attribute>();
                foreach (Attribute attr in prop.GetCustomAttributes(true))
                {
                    var attrType = attr.GetType();
                    var superClassType = typeof(BaseAttribute);
                    if (Array.IndexOf(attrType.GetInterfaces(), superClassType) > -1
                        || attrType.IsSubclassOf(superClassType))
                    {
                        attributes.Add(attr);
                    }
                }

                if (attributes.Any())
                {
                    //IsChkParamAttrDic.TryAdd(prop, attributes);
                    IsChkParamAttrDic.AddOrUpdate(prop, attributes, (key, val) => attributes);

                    return ChkAllAttr(attributes, arg, prop);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 检查所有特性
        /// </summary>
        /// <param name="attrs">特性集合</param>
        /// <param name="arg">参数值</param>
        /// <param name="prop">属性</param>
        /// <returns>检查结果，若失败则返回错误信息</returns>
        private string ChkAllAttr(List<Attribute> attrs, object arg, PropertyInfo prop)
        {
            StringBuilder errMsg = new StringBuilder();
            foreach (Attribute attr in attrs)
            {
                var attrErrMsg = SingleAttrChk(attr, arg, prop);
                if (!string.IsNullOrWhiteSpace(attrErrMsg))
                {
                    // 说明有错误信息
                    if (_type == ValidType.Fast)
                    {
                        // 快速检查，立即返回错误信息
                        return attrErrMsg;
                    }

                    // 全部检查完毕后才返回错误信息
                    errMsg.Append(attrErrMsg + Environment.NewLine);
                }
            }
            return errMsg.ToString();
        }

        /// <summary>
        /// 单个特性校验
        /// </summary>
        /// <param name="attr">特性</param>
        /// <param name="arg">值</param>
        /// <param name="prop">属性</param>
        /// <returns>校验结果，若失败则返回错误信息</returns>
        private string SingleAttrChk(Attribute attr, object arg, PropertyInfo prop)
        {
            var baseAttribute = attr as BaseAttribute;
            if (baseAttribute == null)
            {
                return null;
            }
            try
            {
                if (baseAttribute.Group != _group)
                {
                    // 非当前组特性，不用校验参数
                    return null;
                }
                var validateRes = baseAttribute.Validate(arg, prop);
                if (!validateRes)
                {
                    return baseAttribute.Message;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return null;
        }

        /// <summary>
        /// 生成类型默认值
        /// </summary>
        /// <param name="targetType">类型</param>
        /// <param name="msg">Result类型的错误信息</param>
        /// <returns>类型默认值</returns>
        internal static object DefaultForType(Type targetType, string msg = null)
        {
            Type commonResultType = GeType("Th.Util.Common", "Th.Util.Common.Result");
            if (commonResultType != null && (commonResultType == targetType || targetType.BaseType == commonResultType))
            {
                var instance = Activator.CreateInstance(targetType);
                //给Message属性赋值 
                PropertyInfo pi0 = targetType.GetProperty("IsSucceed");
                if (pi0 != null) pi0.SetValue(instance, false, null);

                //给Message属性赋值 
                PropertyInfo pi1 = targetType.GetProperty("Message");
                if (pi1 != null) pi1.SetValue(instance, msg ?? string.Empty, null);
                return instance;
            }

            throw new ConstraintViolationException(msg);
        }

        /// <summary>
        /// 生成类型默认值
        /// </summary>
        /// <param name="targetType">类型</param>
        /// <param name="msg">Result类型的错误信息</param>
        /// <returns>类型默认值</returns>
        internal static object AsyncDefaultForType(Type targetType, string msg = null)
        {
            var firstType = targetType.GenericTypeArguments.First();
            Type commonResultType = GeType("Th.Util.Common", "Th.Util.Common.Result");
            if (commonResultType != null
                && (firstType == commonResultType || firstType.BaseType == commonResultType))
            {
                var instance = Activator.CreateInstance(firstType);
                //给Message属性赋值 
                PropertyInfo pi0 = firstType.GetProperty("IsSucceed");
                if (pi0 != null) pi0.SetValue(instance, false, null);

                //给Message属性赋值 
                PropertyInfo pi1 = firstType.GetProperty("Message");
                if (pi1 != null) pi1.SetValue(instance, msg ?? string.Empty, null);
                //return instance;

                MethodInfo mi = typeof(Common).GetMethod("ToTask");
                MethodInfo miConstructed = mi.MakeGenericMethod(firstType);
                object[] args = { instance };
                return miConstructed.Invoke(null, args);
            }

            throw new ConstraintViolationException(msg);
        }

        internal static Type GeType(string assembly, string typeName)
        {
            try
            {
                var type = Assembly.Load(assembly).GetType(typeName);
                return type;
            }
            catch (Exception)
            {

            }
            return null;
        }
    }

    /// <summary>
    /// 验证类型
    /// </summary>
    public enum ValidType
    {
        /// <summary>
        /// 快速验证，即验证的参数中，有任意一个不符合规则就返回
        /// </summary>
        Fast = 1,

        /// <summary>
        /// 全部验证，即将所有参数校验完成后才将所有未通过校验的参数的错误信息返回。
        /// </summary>
        All = 2,
    }
}
