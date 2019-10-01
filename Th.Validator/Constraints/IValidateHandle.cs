using System;
using System.Collections.Generic;
using System.Text;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证处理
    /// </summary>
    public interface IValidateHandle
    {
        ///// <summary>
        ///// 返回的错误信息
        ///// </summary>
        //string Message { get; set; }

        ///// <summary>
        ///// 分组，用于解决同一个参数的校验方式在不同业务中使用不同规则
        ///// </summary>
        //string Group { get; set; }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>符合要求=true</returns>
        bool Validate(object value);
    }
}
