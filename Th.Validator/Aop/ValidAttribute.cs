using System;
using System.Collections.Generic;
using System.Text;
using ArxOne.MrAdvice.Advice;

namespace Th.Validator.Aop
{
    /// <summary>
    /// 参数验证
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ValidAttribute : Attribute, IMethodAdvice
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        private ValidType _type { get; set; }

        /// <summary>
        /// 要验证的参数，不传默认为验证当前方法入参的所有参数
        /// </summary>
        private string _paramNames { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">验证类型</param>
        /// <param name="paramName">要验证的参数，不传默认为验证当前方法入参的所有参数</param>
        public ValidAttribute(string paramName = "", ValidType type = ValidType.Fast)
        {
            _type = type;
            _paramNames = paramName;
        }

        /// <summary>
        /// Implements advice logic.
        /// Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The method advice context.</param>
        public void Advise(MethodAdviceContext context)
        {
            StringBuilder errorMsg = new StringBuilder();
            try
            {
                context.Proceed();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    /// <summary>
    /// 验证类型
    /// </summary>
    public enum ValidType
    {
        /// <summary>
        /// 快速验证，即验证的参数中，有任意一个不符合规则就返回
        /// </summary>
        Fast = 1,

        /// <summary>
        /// 全部验证，即将所有参数校验完成后才将所有未通过校验的参数的错误信息返回。
        /// </summary>
        All = 2,
    }
}
