using System;
using System.Collections;
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
            if (prop.PropertyType == typeof(string))
            {
                // 字符串，判断非Null，且长度大于零
                return value != null && ((string)value).Length > 0;
            }
            if (prop.PropertyType.IsCollectionType())
            {
                // 集合类型，判断非Null，且集合个数大于零
                return value != null && ((ICollection)value).Count > 0;
            }
            return false;
        }
    }
}
