using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证参数必须为false
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class AssertFalseAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public AssertFalseAttribute(string message)
        {
            this.Message = message;
        }

        public override bool Validate(object value)
        {
            return false;
        }
    }
}
