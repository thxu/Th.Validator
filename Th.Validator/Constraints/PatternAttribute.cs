using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值与指定的正则表达式匹配
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PatternAttribute : BaseAttribute
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        private string _regex;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="regex">正则表达式</param>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>

        public PatternAttribute(string regex, string msg, string @group = "") : base(msg, @group)
        {
            _regex = regex;
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            string str = (string)value;
            var isMatch = Regex.IsMatch(str, _regex, RegexOptions.ECMAScript);
            return isMatch;
        }
    }
}
