using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class MaxModel
    {
        [Max(100.01, "必须小于100.01")]
        public float FloatField { get; set; }

        [Max(100.01, "必须小于或等于100.01", true)]
        public float FloatField1 { get; set; }
    }

    public class MinModel
    {
        [Min(100.01, "必须大于100.01")]
        public float FloatField { get; set; }

        [Min(100.01, "必须大于或等于100.01,", true)]
        public float FloatField1 { get; set; }
    }
}
