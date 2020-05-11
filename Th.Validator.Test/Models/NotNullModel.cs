using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class NotNullModel
    {
        [NotNull("字符串不能为null")]
        public string StrField { get; set; }
    }

    public class NullModel
    {
        [Null("字符串必须为null")]
        public string StrField { get; set; }
    }
}
