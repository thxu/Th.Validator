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
        /// Implements advice logic.
        /// Usually, advice must invoke context.Proceed()
        /// </summary>
        /// <param name="context">The method advice context.</param>
        public void Advise(MethodAdviceContext context)
        {
        }
    }
}
