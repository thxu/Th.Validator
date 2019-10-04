using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值的在min和max（包含）指定区间之内，如字符长度、集合大小
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SizeAttribute : BaseAttribute
    {
        /// <summary>
        /// 下限
        /// </summary>
        private int _min;

        /// <summary>
        /// 是否包含了最小值
        /// </summary>
        private bool _isIncludeMin;

        /// <summary>
        /// 上限
        /// </summary>
        private int _max;
        /// <summary>
        /// 是否包含了最大值
        /// </summary>
        private bool _isIncludeMax;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        /// <param name="isIncludeMin">是否包含了最小值</param>
        /// <param name="isIncludeMax">是否包含了最大值</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>

        public SizeAttribute(int min, int max, string msg, bool isIncludeMin = false, bool isIncludeMax = false, string @group = "") : base(msg, @group)
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
            if (value == null)
            {
                return false;
            }
            if (prop.PropertyType == typeof(string))
            {
                string str = (string)value;
                bool flg1 = _isIncludeMin ? _min <= str.Length : _min < str.Length;
                bool flg2 = _isIncludeMax ? str.Length <= _max : str.Length < _max;
                return flg1 && flg2;
            }
            if (prop.PropertyType.IsArray)
            {
                Array array = (Array)value;
                bool flg1 = _isIncludeMin ? _min <= array.Length : _min < array.Length;
                bool flg2 = _isIncludeMax ? array.Length <= _max : array.Length < _max;
                return flg1 && flg2;
            }

            return false;
        }
    }
}
