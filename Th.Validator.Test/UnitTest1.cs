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
            new TestLogic().AddTest(new TestModelA() { Field1 = false, Field2 = true, Field3 = 1 });
            new TestLogic().AddTest(new TestModelA() { Field1 = true, Field2 = true, Field3 = 1 });

            int a = 0;
            Assert.IsTrue(true);
        }
    }

    public class TestLogic
    {
        [ValidateParam]
        public int AddTest(TestModelA a)
        {
            return 1;
        }
    }

    public class TestModelA
    {
        [AssertFalse("字段1不能为true")]
        [DataMember]
        public bool Field1 { get; set; }

        public bool Field2 { get; set; }

        public int Field3 { get; set; }

        public TestModelB Field4 { get; set; }
    }

    public class TestModelB
    {
        [AssertFalse("B字段1不能为true")]
        [DataMember]
        public bool BField1 { get; set; }

        public bool BField2 { get; set; }

        public int BField3 { get; set; }
    }
}
