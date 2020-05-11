using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class RangeModel
    {
        [Range(1, 100, "值必须在[1,100)之间", true)]
        public decimal DecimalField { get; set; }
    }
}
