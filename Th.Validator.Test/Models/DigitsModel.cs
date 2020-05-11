using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class DigitsModel
    {
        [Digits(3, 3, "位数超过限制")]
        [Digits(2, 2, "位数超过限制", "Test2Group")]
        public decimal DecimalField { get; set; }
    }
}
