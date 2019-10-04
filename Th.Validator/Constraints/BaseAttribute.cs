using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 基础特性，所有的自定义特性都要继承此特性
    /// </summary>
    public abstract class BaseAttribute : Attribute
    {
        /// <summary>
        /// 返回的错误信息
        /// </summary>
        internal string Message { get; set; }

        /// <summary>
        /// 分组，用于解决同一个参数的校验方式在不同业务中使用不同规则
        /// </summary>
        internal string Group { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        protected BaseAttribute(string msg, string group = "")
        {
            this.Message = msg;
            this.Group = group;
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public abstract bool Validate(object value, PropertyInfo prop);
    }
}
