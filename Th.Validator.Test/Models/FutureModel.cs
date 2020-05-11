using System;
using System.Collections.Generic;
using System.Text;
using Th.Validator.Constraints;

namespace Th.Validator.Test.Models
{
    public class FutureModel
    {
        [Future("日期必须大于当前时间")]
        public DateTime DatetimeField { get; set; }

        [Future("日期必须大于或等于2019-09-01", "2019-09-01", true)]
        public DateTime DatetimeField1 { get; set; }
    }

    public class PastModel
    {
        [Past("日期不能大于当前时间")]
        public DateTime DatetimeField { get; set; }

        [Past("日期不能大于2019-09-01", "2019-09-01", true)]
        public DateTime DatetimeField1 { get; set; }
    }
}
