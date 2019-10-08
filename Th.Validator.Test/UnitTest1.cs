using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

                    Field5 = new List<TestModelB>
                    {
                        new TestModelB()
                        {
                            BField3 = 1,
                            //BField1 = true,
                        },
                        new TestModelB()
                        {
                            BField3 = 2,
                            BField1 = true,
                        },
                    }
                    //Field5 = null,
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
            //Queue<string>; : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection;
            //ArrayList; : IEnumerable, IList, ICollection, ICloneable;
            //List<string>; : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, IList;
            //LinkedList; : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICollection, IDeserializationCallback, ISerializable;
            //Dictionary<string, string> a; : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary, IDeserializationCallback, ISerializable;
            //Hashtable a;: ICollection, IEnumerable, IDictionary, ISerializable, IDeserializationCallback, ICloneable;
            //SortedList; : ICollection, IEnumerable, IDictionary, ICloneable;
            //Stack;: ICollection, IEnumerable, ICloneable;
            //SortedList<string, string>;: ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary;
            //HashSet<strign>;: ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ISet<T>, IDeserializationCallback, ISerializable;
            //SortedSet<string>;: ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ISet<T>, ICollection, IDeserializationCallback, ISerializable;
            //BitArray;: ICollection, IEnumerable, ICloneable;
            //ListDictionary;: ICollection, IEnumerable, IDictionary;
            //HybridDictionary;: ICollection, IEnumerable, IDictionary;

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

        [TestMethod]
        public void TestDemo()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {

            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void AssertFalseTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                AssertFalseModel model = new AssertFalseModel
                {
                    BoolField = false,
                };
                res = new TestLogic().AssertFalseTest(model, 0, 0);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            res = 0;
            errMsg = string.Empty;
            try
            {
                AssertFalseModel model = new AssertFalseModel
                {
                    BoolField = true,
                };
                res = new TestLogic().AssertFalseTest(model, 0, 0);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res == 0);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void AssertTrueTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                AssertTrueModel model = new AssertTrueModel
                {
                    BoolField = true,
                };
                res = new TestLogic().AssertTrueTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            res = 0;
            errMsg = string.Empty;
            try
            {
                AssertTrueModel model = new AssertTrueModel
                {
                    BoolField = false,
                };
                res = new TestLogic().AssertTrueTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res == 0);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void DigitsTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                DigitsModel model = new DigitsModel
                {
                    DecimalField = 10.01m,
                };

                res = new TestLogic().DigitsTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;
            try
            {
                DigitsModel model = new DigitsModel
                {
                    DecimalField = 1002.0102m,
                };

                res = new TestLogic().DigitsTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));

            /****************************************************/

            errMsg = string.Empty;
            res = 0;

            try
            {
                DigitsModel model = new DigitsModel
                {
                    DecimalField = 1.1m,
                };

                res = new TestLogic().DigitsTest2(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;
            try
            {
                DigitsModel model = new DigitsModel
                {
                    DecimalField = 100.002m,
                };

                res = new TestLogic().DigitsTest2(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void EmailTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                EmailModel model = new EmailModel
                {
                    EmailField = "123@qq.com"
                };
                res = new TestLogic().EmailTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                EmailModel model = new EmailModel
                {
                    EmailField = "1�з�23@qq.com"
                };
                res = new TestLogic().EmailTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void FutureTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                FutureModel model = new FutureModel
                {
                    DatetimeField = new DateTime(2019, 12, 01),
                    DatetimeField1 = DateTime.MaxValue,
                };
                res = new TestLogic().FutureTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                FutureModel model = new FutureModel
                {
                    DatetimeField = new DateTime(2019, 10, 05),
                    DatetimeField1 = DateTime.MaxValue,
                };
                res = new TestLogic().FutureTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));

            /**********************************************/

            errMsg = string.Empty;
            res = 0;

            try
            {
                FutureModel model = new FutureModel
                {
                    DatetimeField = DateTime.MaxValue,
                    DatetimeField1 = new DateTime(2019, 09, 01),
                };
                res = new TestLogic().FutureTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                FutureModel model = new FutureModel
                {
                    DatetimeField = DateTime.MaxValue,
                    DatetimeField1 = new DateTime(2019, 01, 05),
                };
                res = new TestLogic().FutureTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void MaxTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                MaxModel model = new MaxModel
                {
                    FloatField = 10.01f,
                    FloatField1 = 1f,
                };
                res = new TestLogic().MaxTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                MaxModel model = new MaxModel
                {
                    FloatField = 100.01f,
                    FloatField1 = 1f,
                };
                res = new TestLogic().MaxTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));

            /*********************************************************************************/

            errMsg = string.Empty;
            res = 0;

            try
            {
                MaxModel model = new MaxModel
                {
                    FloatField = 10.01f,
                    FloatField1 = 100.01f,
                };
                res = new TestLogic().MaxTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                MaxModel model = new MaxModel
                {
                    FloatField = 10.01f,
                    FloatField1 = 1000.01f,
                };
                res = new TestLogic().MaxTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void NotBlankTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                NotBlankModel model = new NotBlankModel
                {
                    StrField = " sdf ",
                };
                res = new TestLogic().NotBlankTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                NotBlankModel model = new NotBlankModel
                {
                    StrField = "  ",
                };
                res = new TestLogic().NotBlankTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                NotBlankModel model = new NotBlankModel
                {
                    StrField = null,
                };
                res = new TestLogic().NotBlankTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void NotEmptyTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                NotEmptyModel model = new NotEmptyModel
                {
                    StrField = " ",
                    StrFields = new List<string>() { " " },
                };
                res = new TestLogic().NotEmptyTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                NotEmptyModel model = new NotEmptyModel
                {
                    StrField = "",
                    StrFields = new List<string>() { " " },
                };
                res = new TestLogic().NotEmptyTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                NotEmptyModel model = new NotEmptyModel
                {
                    StrField = " ",
                    StrFields = new List<string>(),
                };
                res = new TestLogic().NotEmptyTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void NotNullTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                NotNullModel model = new NotNullModel
                {
                    StrField = " ",
                };
                res = new TestLogic().NotNullTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                NotNullModel model = new NotNullModel
                {
                    StrField = null,
                };
                res = new TestLogic().NotNullTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void NullTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                NullModel model = new NullModel
                {
                    StrField = null,
                };
                res = new TestLogic().NullTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                NullModel model = new NullModel
                {
                    StrField = "",
                };
                res = new TestLogic().NullTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void RangeTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                RangeModel model = new RangeModel
                {
                    DecimalField = 2m,
                };
                res = new TestLogic().RangeTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                RangeModel model = new RangeModel
                {
                    DecimalField = 200m,
                };
                res = new TestLogic().RangeTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }

        [TestMethod]
        public void SizeTest()
        {
            string errMsg = string.Empty;
            int res = 0;

            try
            {
                SizeModel model = new SizeModel
                {
                    StrField = "12345",
                    IntFields = new List<int>() { 1,2,3,4},
                };
                res = new TestLogic().SizeTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                SizeModel model = new SizeModel
                {
                    StrField = "123",
                    IntFields = new List<int>() { 1, 2, 3, 4 },
                };
                res = new TestLogic().SizeTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));

            /*************************************************************/

            errMsg = string.Empty;
            res = 0;

            try
            {
                SizeModel model = new SizeModel
                {
                    StrField = "12345",
                    IntFields = new List<int>() { 1, 2, 3, 4 },
                };
                res = new TestLogic().SizeTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res == 100);
            Assert.IsTrue(string.IsNullOrEmpty(errMsg));

            errMsg = string.Empty;
            res = 0;

            try
            {
                SizeModel model = new SizeModel
                {
                    StrField = "12345",
                    IntFields = new List<int>() { 1, 2},
                };
                res = new TestLogic().SizeTest(model);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            Assert.IsTrue(res != 100);
            Assert.IsTrue(!string.IsNullOrEmpty(errMsg));
        }
    }

    public class TestLogic
    {
        [ValidateParam("a-b")]
        public int AddTest(TestModelA a, int b)
        {
            return 1;
        }

        [ValidateParam("model")]
        public int AssertFalseTest(AssertFalseModel model, int a, int b)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int AssertTrueTest(AssertTrueModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int DigitsTest(DigitsModel model)
        {
            return 100;
        }

        [ValidateParam("model", "Test2Group")]
        public int DigitsTest2(DigitsModel model)
        {
            return 100;
        }


        [ValidateParam("model")]
        public int EmailTest(EmailModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int FutureTest(FutureModel model)
        {
            return 100;
        }
        [ValidateParam("model")]
        public int PastTest(PastModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int MaxTest(MaxModel model)
        {
            return 100;
        }
        [ValidateParam("model")]
        public int MinTest(MinModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int NotBlankTest(NotBlankModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int NotEmptyTest(NotEmptyModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int NotNullTest(NotNullModel model)
        {
            return 100;
        }
        [ValidateParam("model")]
        public int NullTest(NullModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int RangeTest(RangeModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public int SizeTest(SizeModel model)
        {
            return 100;
        }
    }

    public class TestModelA
    {
        //public string[] Field02 { get; set; }
        ////public String Field0 { get; set; }

        //public Int32[] Field01 { get; set; }

        //[NotNull("�ֶ�1����ΪNull")]
        //[AssertFalse("�ֶ�1����Ϊtrue")]
        //[DataMember]
        //public bool? Field1 { get; set; }

        ////public bool Field2 { get; set; }

        ////public int Field3 { get; set; }

        //public TestModelB Field4 { get; set; }
        [NotNull("�ֶ�5����ΪNull")]
        [InnerValid]
        public IList<TestModelB> Field5 { get; set; }
    }

    public class TestModelB
    {
        [AssertFalse("B�ֶ�1����Ϊtrue")]
        [DataMember]
        public bool BField1 { get; set; }

        public bool BField2 { get; set; }

        public int BField3 { get; set; }
    }

    public class AssertFalseModel
    {
        [AssertFalse("�ֶβ���Ϊtrue")]
        public bool BoolField { get; set; }
    }
    public class AssertTrueModel
    {
        [AssertTrue("�ֶβ���Ϊfalse")]
        public bool BoolField { get; set; }
    }

    public class DigitsModel
    {
        [Digits(3, 3, "λ����������")]
        [Digits(2, 2, "λ����������", "Test2Group")]
        public decimal DecimalField { get; set; }
    }

    public class EmailModel
    {
        [Email("�ʼ���ʽ����ȷ")]
        public string EmailField { get; set; }
    }

    public class FutureModel
    {
        [Future("���ڱ�����ڵ�ǰʱ��")]
        public DateTime DatetimeField { get; set; }

        [Future("���ڱ�����ڻ����2019-09-01", "2019-09-01", true)]
        public DateTime DatetimeField1 { get; set; }
    }

    public class PastModel
    {
        [Past("���ڲ��ܴ��ڵ�ǰʱ��")]
        public DateTime DatetimeField { get; set; }

        [Past("���ڲ��ܴ���2019-09-01", "2019-09-01", true)]
        public DateTime DatetimeField1 { get; set; }
    }

    public class MaxModel
    {
        [Max(100.01, "����С��100.01")]
        public float FloatField { get; set; }

        [Max(100.01, "����С�ڻ����100.01", true)]
        public float FloatField1 { get; set; }
    }

    public class MinModel
    {
        [Min(100.01, "�������100.01")]
        public float FloatField { get; set; }

        [Min(100.01, "������ڻ����100.01,", true)]
        public float FloatField1 { get; set; }
    }

    public class NotBlankModel
    {
        [NotBlank("Ԫ�ر���Ϊ�ǿ��ַ�")]
        public string StrField { get; set; }
    }

    public class NotEmptyModel
    {
        [NotEmpty("�ַ�������Ϊnull���ַ��������������")]
        public string StrField { get; set; }

        [NotEmpty("���ϲ���Ϊnull����������Ҫ��һ��Ԫ��")]
        public IList<string> StrFields { get; set; }
    }

    public class NotNullModel
    {
        [NotNull("�ַ�������Ϊnull")]
        public string StrField { get; set; }
    }

    public class NullModel
    {
        [Null("�ַ�������Ϊnull")]
        public string StrField { get; set; }
    }

    public class RangeModel
    {
        [Range(1, 100, "ֵ������[1,100)֮��", true)]
        public decimal DecimalField { get; set; }
    }

    public class SizeModel
    {
        [Size(4, 10, "�ַ������ȱ�����(4,10]֮��", false, true)]
        public string StrField { get; set; }

        [Size(3, 6, "���ϸ���������(3,6)֮��")]
        public List<int> IntFields { get; set; }
    }
}
