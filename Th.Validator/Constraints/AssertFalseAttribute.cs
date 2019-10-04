using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证参数必须为false
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AssertFalseAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public AssertFalseAttribute(string message, string group = "") : base(message, group)
        {
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            if (prop.PropertyType != typeof(bool) && prop.PropertyType != typeof(bool?))
            {
                return false;
            }
            if (prop.PropertyType == typeof(bool?) && value == null)
            {
                return true;
            }
            return (bool)value == false;
        }
    }
}
