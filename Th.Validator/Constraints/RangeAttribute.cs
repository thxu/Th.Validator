using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值在最小值和最大值之间
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeAttribute : BaseAttribute
    {
        /// <summary>
        /// 最小值
        /// </summary>
        private decimal _min;

        /// <summary>
        /// 是否包含了最小值
        /// </summary>
        private bool _isIncludeMin;

        /// <summary>
        /// 最大值
        /// </summary>
        private decimal _max;

        /// <summary>
        /// 是否包含了最大值
        /// </summary>
        private bool _isIncludeMax;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="isIncludeMin">是否包含了最小值</param>
        /// <param name="isIncludeMax">是否包含了最大值</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public RangeAttribute(int min, int max, string msg, bool isIncludeMin = false, bool isIncludeMax = false, string @group = "") : base(msg, @group)
        {
            _min = min;
            _max = max;
            _isIncludeMin = isIncludeMin;
            _isIncludeMax = isIncludeMax;
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            decimal dec = (decimal)value;
            var flg1 = _isIncludeMin ? _min <= dec : _min < dec;
            var flg2 = _isIncludeMax ? dec <= _max : dec < _max;

            return flg1 && flg2;
        }
    }
}
