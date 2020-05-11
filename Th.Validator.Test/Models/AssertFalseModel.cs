using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class AssertFalseModel
    {
        [AssertFalse("字段必须为false")]
        public bool BoolField { get; set; }
    }

    public class AssertTrueModel
    {
        [AssertTrue("字段必须为true")]
        public bool BoolField { get; set; }
    }
}
