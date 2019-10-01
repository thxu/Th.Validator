using System;
using System.Collections.Generic;
using System.Text;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值小于等于指定的value值
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MaxAttribute : BaseAttribute
    {
        /// <summary>
        /// 要比较的值
        /// </summary>
        private decimal _value;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">要比较的值</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public MaxAttribute(decimal value, string msg, string @group = "") : base(msg, @group)
        {
            _value = value;
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value)
        {
            return false;
        }
    }
}
