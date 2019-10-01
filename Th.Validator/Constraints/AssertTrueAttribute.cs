using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证参数必须为True
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AssertTrueAttribute : BaseAttribute
    {
        public AssertTrueAttribute(string msg, string @group = "") : base(msg, @group)
        {
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value)
        {
            return (bool)value == true;
        }
    }
}
