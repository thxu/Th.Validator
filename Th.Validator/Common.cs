using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ArxOne.MrAdvice.Advice;

namespace Th.Validator
{
    internal static class Common
    {
        /// <summary>
        /// 获取入参
        /// </summary>
        /// <param name="context">函数调用上下文</param>
        /// <returns>输入参数json字符串</returns>
        public static string GetInParam(this MethodAdviceContext context)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            IList<object> arguments = context.Arguments;
            ParameterInfo[] parameters = context.TargetMethod.GetParameters();
            if (parameters.Length <= 0)
            {
                return string.Empty;
            }
            for (int i = 0; arguments != null && i < arguments.Count; i++)
            {
                //res.Add(parameters[i].Name, arguments[i].ToJson());
            }

            if (res.Count <= 0)
            {
                return string.Empty;
            }
            //return res.ToJson();
            return null;
        }

        /// <summary>
        /// 获取返回参数
        /// </summary>
        /// <param name="context">函数调用上下文</param>
        /// <returns>返回参数</returns>
        public static string GetOutParam(this MethodAdviceContext context)
        {
            if (!context.HasReturnValue)
            {
                return string.Empty;
            }
            //return context.ReturnValue?.ToJson();
            return null;
        }


    }
}
