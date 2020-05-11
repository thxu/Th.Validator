using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class NotBlankModel
    {
        [NotBlank("元素必须为非空字符")]
        public string StrField { get; set; }
    }
}
