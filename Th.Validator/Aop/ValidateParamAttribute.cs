using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public ValidateParamAttribute(string paramName = "",string group = "", ValidType type = ValidType.Fast)
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
                IList<object> arguments = context.Arguments;
                ParameterInfo[] parameters = context.TargetMethod.GetParameters();

                var paramType = parameters[0].ParameterType;
                var fieldsProp = paramType.GetProperties();

                var chkRes = ChkAllProp(fieldsProp.ToList(), arguments[0]);
                if (!string.IsNullOrWhiteSpace(chkRes))
                {
                    // 参数不符合要求
                    throw new ConstraintViolationException(chkRes);
                }

                var arg = arguments[0];
                var tt = arg.GetType();

                var ttt = "a.Field1".GetComplexVal(context);


                context.Proceed();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private string ChkAllProp(List<PropertyInfo> props, object arg)
        {
            if (props == null || !props.Any())
            {
                return null;
            }

            foreach (PropertyInfo prop in props)
            {
                var name = prop.Name;
                var val = prop.GetValue(arg, null);
                if (prop.PropertyType.IsNeedRecursionChk())
                {
                    if (val != null)
                    {
                        if (prop.PropertyType.IsArray)
                        {
                            var elementType = prop.PropertyType.GetElementType();
                            if (elementType != null)
                            {
                                foreach (object arrVal in (Array)val)
                                {
                                    var errMsg = elementType.IsNeedRecursionChk()
                                        ? ChkAllProp(elementType.GetProperties().ToList(), arrVal)
                                        : SinglePropChk(prop, arrVal);
                                    if (!string.IsNullOrWhiteSpace(errMsg))
                                    {
                                        return errMsg;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var errMsg = ChkAllProp(prop.PropertyType.GetProperties().ToList(), val);
                            if (!string.IsNullOrWhiteSpace(errMsg))
                            {
                                return errMsg;
                            }
                        }
                    }
                    else
                    {
                        var errMsg = SinglePropChk(prop, null);
                        if (!string.IsNullOrWhiteSpace(errMsg))
                        {
                            return errMsg;
                        }
                    }
                }
                else
                {
                    // 无需递归检查参数内部，开始执行当前参数检查
                    var errMsg = SinglePropChk(prop, val);
                    if (!string.IsNullOrWhiteSpace(errMsg))
                    {
                        return errMsg;
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
                return ChkAllAttr(IsChkParamAttrDic[prop], arg);
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

                    return ChkAllAttr(attributes, arg);
                }
            }

            return string.Empty;
        }

        private string ChkAllAttr(List<Attribute> attrs, object arg)
        {
            StringBuilder errMsg = new StringBuilder();
            foreach (Attribute attr in attrs)
            {
                var attrErrMsg = SingleAttrChk(attr, arg);
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
        /// <returns>校验结果，若失败则返回错误信息</returns>
        private string SingleAttrChk(Attribute attr, object arg)
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
                var validateRes = baseAttribute.Validate(arg);
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
