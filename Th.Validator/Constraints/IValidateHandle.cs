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

        bool Validate(object value);
    }
}
