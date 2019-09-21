using System;
using System.Collections.Generic;
using System.Text;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证参数必须为false
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AssertFalseAttribute : Attribute, IErrorMsg
    {
        /// <summary>
        /// 返回的错误信息
        /// </summary>
        public string Message { get; set; }
    }
}
