using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Th.Validator.Aop;
using Th.Validator.Constraints;

namespace Th.Validator.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new TestLogic()
                .AddTest(new TestModelA()
                {
                    //Field01 = new[] { 1, 2, 3 },
                    //Field1 = false,
                    //Field2 = true,
                    //Field3 = 1,
                    //Field02 = new[] { "11", "22" },
                    //Field4 = new TestModelB()
                    //{
                    //    BField1 = true,
                    //    BField2 = true,
                    //    BField3 = 3,
                    //},

                    //Field5 = new List<TestModelB>
                    //{
                    //    new TestModelB()
                    //    {
                    //        BField3 = 1,
                    //        //BField1 = true,
                    //    },
                    //    new TestModelB()
                    //    {
                    //        BField3 = 2,
                    //        BField1 = true,
                    //    },
                    //}
                    Field5 = null,
                }, 1);
            //new TestLogic()
            //    .AddTest(new TestModelA()
            //    {
            //        Field1 = true,
            //        //Field2 = true,
            //        //Field3 = 1
            //    });

            int a = 0;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Test2()
        {
            sbyte s = 127;
            decimal dec = (decimal)s;
            string tmp = dec.ToString("0.#");

            //var numberofint = ((long)dec).NumberOfIntegerDigits();
            //var numberofdec = ((decimal)dec).NumberOfDecimalDigits();


            dec = 12.30200m;
            //numberofint = ((long)dec).NumberOfIntegerDigits();
            //numberofdec = ((decimal)dec).NumberOfDecimalDigits();
            tmp = dec.ToString();

            dec = 0.0030m;
            //numberofint = ((long)dec).NumberOfIntegerDigits();
            //numberofdec = ((decimal)dec).NumberOfDecimalDigits();
            tmp = dec.ToString();
        }
    }

    public class TestLogic
    {
        [ValidateParam("a-b")]
        public int AddTest(TestModelA a, int b)
        {
            return 1;
        }
    }

    public class TestModelA
    {
        //public string[] Field02 { get; set; }
        ////public String Field0 { get; set; }

        //public Int32[] Field01 { get; set; }

        //[NotNull("字段1不能为Null")]
        //[AssertFalse("字段1不能为true")]
        //[DataMember]
        //public bool? Field1 { get; set; }

        ////public bool Field2 { get; set; }

        ////public int Field3 { get; set; }

        //public TestModelB Field4 { get; set; }
        //[NotNull("字段5不能为Null")]
        public IList<TestModelB> Field5 { get; set; }
    }

    public class TestModelB
    {
        [AssertFalse("B字段1不能为true")]
        [DataMember]
        public bool BField1 { get; set; }

        public bool BField2 { get; set; }

        public int BField3 { get; set; }
    }

    public enum enum1
    {
        a = 0,

        b = 2,

    }
}
