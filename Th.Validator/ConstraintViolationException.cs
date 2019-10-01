using System;

namespace Th.Validator
{
    /// <summary>
    /// 参数值校验失败异常 
    /// </summary>
    [Serializable]
    public class ConstraintViolationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintViolationException"/> class.
        /// </summary>
        public ConstraintViolationException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintViolationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ConstraintViolationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintViolationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public ConstraintViolationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
