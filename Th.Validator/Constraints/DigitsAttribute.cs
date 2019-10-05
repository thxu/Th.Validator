using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值的整数位数和小数位数上限
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DigitsAttribute : BaseAttribute
    {
        /// <summary>
        /// 整数位数
        /// </summary>
        private int _integer;

        /// <summary>
        /// 小数位数
        /// </summary>
        private int _fraction;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="integer">整数位数</param>
        /// <param name="fraction">小数位数</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public DigitsAttribute(int integer, int fraction, string msg, string @group = "") : base(msg, @group)
        {
            _integer = integer;
            _fraction = fraction;
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
            
            return ((long)dec).NumberOfIntegerDigits() <= _integer && dec.NumberOfDecimalDigits() <= _fraction;
        }
    }
}
