using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值大于等于指定的value值
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MinAttribute : BaseAttribute
    {
        /// <summary>
        /// 要比较的值
        /// </summary>
        private decimal _value;

        /// <summary>
        /// 是否包含传入的值
        /// </summary>
        private bool _isInclude;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">要比较的值</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="isInclude">是否包含传入的值</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>

        public MinAttribute(double value, string msg, bool isInclude = false, string @group = "") : base(msg, @group)
        {
            _value = Convert.ToDecimal(value);
            _isInclude = isInclude;
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            if (!prop.PropertyType.IsNumericType())
            {
                return false;
            }
            decimal dec = Convert.ToDecimal(value);
            return _isInclude ? dec >= _value : dec > _value;
        }
    }
}
