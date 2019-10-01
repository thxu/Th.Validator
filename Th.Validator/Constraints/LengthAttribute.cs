using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值长度在min和max区间内
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LengthAttribute : BaseAttribute
    {
        /// <summary>
        /// 下限
        /// </summary>
        private int _min;

        /// <summary>
        /// 上限
        /// </summary>
        private int _max;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public LengthAttribute(int min, int max, string msg, string @group = "") : base(msg, @group)
        {
            _min = min;
            _max = max; 
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
