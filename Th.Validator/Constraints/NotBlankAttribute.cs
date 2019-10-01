using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值不为空（不为null、去除首尾空格后长度为0），不同于NotEmpty，NotBlank只应用于字符串且在比较时会去除字符串的首尾空格
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NotBlankAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public NotBlankAttribute(string msg, string @group = "") : base(msg, @group)
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
