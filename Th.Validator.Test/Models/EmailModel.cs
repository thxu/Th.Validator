using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class EmailModel
    {
        [Email("邮件格式不正确")]
        public string EmailField { get; set; }
    }
}
