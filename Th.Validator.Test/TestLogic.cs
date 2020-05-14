using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Th.Util.Common;
using Th.Validator.Aop;
using Th.Validator.Test.Models;

namespace Th.Validator.Test
{
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
        public int PhoneTest(PhoneModel model)
        {
            return 100;
        }

        [ValidateParam("model")]
        public Result PhoneTest1(PhoneModel model)
        {
            return new Result() { IsSucceed = true, Message = "参数验证通过，执行成功" };
        }

        [ValidateParam("model")]
        public async Task<Result> PhoneTest2(PhoneModel model)
        {
            return await Task.Factory.StartNew((() =>
                {
                    return new Result() { IsSucceed = true, Message = "参数验证通过，执行成功" };
                }));
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
        public int NotEmptyTest1(NotEmptyModel1 model)
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
}
