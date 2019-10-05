using System;
using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 验证元素值（日期类型）比当前时间晚
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FutureAttribute : BaseAttribute
    {
        /// <summary>
        /// 要比较的日期
        /// </summary>
        private DateTime _dateTime;

        /// <summary>
        /// 是否包含传入的时间
        /// </summary>
        private bool _isInclude;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="dateTime">要比较的日期(格式：yyyy-MM-dd HH:mm:ss)，不传则默认为DateTime.Now</param>
        /// <param name="isInclude">是否包含传入的时间</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public FutureAttribute(string msg, string dateTime = "", bool isInclude = false, string @group = "") : base(msg, @group)
        {
            _dateTime = string.IsNullOrEmpty(dateTime) ? DateTime.Now : DateTime.Parse(dateTime);
            _isInclude = isInclude;
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            if (prop.PropertyType != typeof(DateTime) && prop.PropertyType != typeof(DateTime?))
            {
                return false;
            }
            if (prop.PropertyType == typeof(DateTime?) && value == null)
            {
                return true;
            }
            DateTime dateTime = (DateTime)value;
            return _isInclude ? dateTime >= _dateTime : dateTime > _dateTime;
        }
    }
}
