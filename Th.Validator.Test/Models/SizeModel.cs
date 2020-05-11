using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class SizeModel
    {
        [Size(4, 10, "字符串长度必须在(4,10]之间", false, true)]
        public string StrField { get; set; }

        [Size(3, 6, "集合个数必须在(3,6)之间")]
        public List<int> IntFields { get; set; }
    }
}
