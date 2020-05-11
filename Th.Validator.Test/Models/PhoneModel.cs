using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class PhoneModel
    {
        [Phone("手机号格式不正确")]
        public string PhoneField { get; set; }
    }
}
