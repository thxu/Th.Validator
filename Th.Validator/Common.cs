using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
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

        /// <summary>
        /// 获取参数中的key值
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="context">函数执行上下文</param>
        /// <returns>参数值</returns>
        internal static object GetComplexVal(this string param, MethodAdviceContext context)
        {
            var paramTmps = param.Split('.');
            ParameterInfo[] parameters = context.TargetMethod.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Name == paramTmps[0])
                {
                    Type ts = context.Arguments[i].GetType();

                    object obj = DeepCopy(context.Arguments[i], ts);
                    for (int j = 1; j < paramTmps.Length; j++)
                    {
                        var val = obj.GetType().GetProperty(paramTmps[j])?.GetValue(obj, null);
                        if (val != null) obj = DeepCopy(val, val.GetType());
                    }

                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// XML序列化方式深复制
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">对象类型</param>
        /// <returns>复制对象</returns>
        internal static object DeepCopy(this object obj, Type type)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(type);
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }

            return retval;
        }

        /// <summary>
        /// 获取指定数组中每个元素的类型
        /// </summary>
        /// <param name="t">数组类型</param>
        /// <returns>元素类型</returns>
        internal static Type GetArrayElementType(this Type t)
        {
            if (!t.IsArray) return null;
            if (t.FullName != null)
            {
                string tName = t.FullName.Replace("[]", string.Empty);
                Type elType = t.Assembly.GetType(tName);
                return elType;
            }

            return null;
        }

        /// <summary>
        /// 判断类型是否需要进行递归检查
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>判断结果，需要递归检查=true</returns>
        internal static bool IsNeedRecursionChk(this Type type)
        {
            return type.IsClass && type != typeof(string);
        }
    }
}
