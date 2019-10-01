using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值是Email，也可以通过regexp和flag指定自定义的email格式
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class EmailAttribute : BaseAttribute
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
        public EmailAttribute(string regex, string msg, string @group = "") : base(msg, @group)
        {
            _regex = regex;
            if (string.IsNullOrWhiteSpace(_regex))
            {
                //_regex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                //_regex = @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?";
                _regex = @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}";
            }
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
