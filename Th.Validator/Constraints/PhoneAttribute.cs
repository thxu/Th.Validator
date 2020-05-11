using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值是手机号，也可以通过regexp和flag指定自定义的email格式
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PhoneAttribute : BaseAttribute
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        private string _regex;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public PhoneAttribute(string msg, string @group = "", string regex = "") : base(msg, @group)
        {
            _regex = regex;
            if (string.IsNullOrWhiteSpace(_regex))
            {
                _regex = @"^1\d{10}$";
            }
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            if (prop.PropertyType != typeof(string))
            {
                return false;
            }
            string str = (string)value;
            var isMatch = Regex.IsMatch(str, _regex, RegexOptions.ECMAScript);
            return isMatch;
        }
    }
}
