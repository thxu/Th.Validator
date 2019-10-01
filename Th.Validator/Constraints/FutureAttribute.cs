using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值（日期类型）比当前时间晚
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FutureAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public FutureAttribute(string msg, string @group = "") : base(msg, @group)
        {
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
