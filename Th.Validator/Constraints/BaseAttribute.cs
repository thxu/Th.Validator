﻿using System;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 基础特性，所有的自定义特性都要继承此特性
    /// </summary>
    public class BaseAttribute : Attribute
    {
        /// <summary>
        /// 返回的错误信息
        /// </summary>
        internal string Message { get; set; }

        /// <summary>
        /// 分组，用于解决同一个参数的校验方式在不同业务中使用不同规则
        /// </summary>
        internal string Group { get; set; }
    }
}
