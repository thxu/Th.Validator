using System;
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
        /// <summary>
        /// 验证类型
        /// </summary>
        private ValidType _type { get; set; }

        /// <summary>
        /// 要验证的参数，不传默认为验证当前方法入参的所有参数
        /// </summary>
        private string _paramNames { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <param name="paramName">要验证的参数，不传默认为验证当前方法入参的所有参数</param>
        public ValidateParamAttribute(string paramName = "", ValidType type = ValidType.Fast)
        {
            _type = type;
            _paramNames = paramName;
        }

        /// <summary>
        /// Implements advice logic.
        /// Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The method advice context.</param>
        public void Advise(MethodAdviceContext context)
        {
            StringBuilder errorMsg = new StringBuilder();
            try
            {
                IList<object> arguments = context.Arguments;
                ParameterInfo[] parameters = context.TargetMethod.GetParameters();

                var paramType = parameters[0].ParameterType;
                var fieldsProp = paramType.GetProperties();

                ScanAllProp(fieldsProp.ToList(), null);

                var arg = arguments[0];
                var tt = arg.GetType();

                var ttt = "a.Field1".GetComplexVal(context);

                foreach (PropertyInfo propertyInfo in fieldsProp)
                {

                    var val = paramType.GetProperty(propertyInfo.Name)?.GetValue(arg, null);


                    if (propertyInfo.CustomAttributes.Any())
                    {
                        //var customAttrs = propertyInfo.CustomAttributes;
                        //foreach (CustomAttributeData customAttributeData in customAttrs)
                        //{
                        //    var type1 = customAttributeData.AttributeType;
                        //    var superClassType = typeof(BaseAttribute);
                        //    if (Array.IndexOf(type1.GetInterfaces(), superClassType) > -1
                        //        || type1.IsSubclassOf(superClassType))
                        //    {
                        //        //var tt = customAttributeData.ConstructorArguments[0].Value;

                        //        var dObj = Activator.CreateInstance(type1, new object[] { "errorMsgOfField1" });
                        //        BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;
                        //        object[] p = new object[] { true };
                        //        var validateMethod = type1.GetMethod("Validate");
                        //        var ret = validateMethod?.Invoke(dObj, flag, Type.DefaultBinder, p, null);

                        //        int a = 0;
                        //    }
                        //}
                        var tmp = propertyInfo.GetCustomAttributes(true);


                        foreach (Attribute attr in tmp)
                        {
                            var type1 = attr.GetType();
                            var superClassType = typeof(BaseAttribute);
                            if (Array.IndexOf(type1.GetInterfaces(), superClassType) > -1
                                || type1.IsSubclassOf(superClassType))
                            {
                                var tmp1 = tmp[0] as BaseAttribute;
                                var tmp2 = tmp1?.Validate(1);

                                //var validateMethod = type1.GetMethod("Validate");
                                //var dObj = Activator.CreateInstance(type1, new object[] { "errorMsgOfField1" });
                                //BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;
                                //object[] p = new object[] { true };
                                //var ret = validateMethod?.Invoke(dObj, flag, Type.DefaultBinder, p, null);

                                //var tt = type1.GetProperty("message")?.GetValue(attr);
                                int a = 0;
                            }
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

        private void ScanAllProp(List<PropertyInfo> props, object arg)
        {
            if (props == null || !props.Any())
            {
                return;
            }

            foreach (PropertyInfo prop in props)
            {
                var name = prop.Name;
                if (prop.PropertyType.IsClass)
                {
                    
                }

                if (prop.PropertyType.IsPrimitive)
                {
                    
                }
            }
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
