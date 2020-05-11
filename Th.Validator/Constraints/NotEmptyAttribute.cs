using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值不为null且不为空（字符串长度不为0、集合大小不为0）
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotEmptyAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public NotEmptyAttribute(string msg, string @group = "") : base(msg, @group)
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
            if (value == null)
            {
                return false;
            }
            if (prop.PropertyType == typeof(string))
            {
                // 字符串，判断非Null，且长度大于零
                return ((string)value).Length > 0;
            }
            if (prop.PropertyType.HasImplementedRawGeneric(typeof(ICollection)))
            {
                // 集合类型，判断非Null，且集合个数大于零
                return ((ICollection)value).Count > 0;
            }
            if (prop.PropertyType.HasImplementedRawGeneric(typeof(ICollection<>)))
            {
                // 集合类型，判断非Null，且集合个数大于零
                var countProp = prop.PropertyType.GetProperty("Count");
                var count = countProp == null
                    ? ((ICollection)value).Count
                    : Convert.ToInt32(countProp.GetValue(value, null));
                return count > 0;
            }
            return false;
        }
    }
}
