using System.Reflection;

namespace Th.Validator.Constraints
{
    /// <summary>
    /// 内部对象级联校验特性，目前仅支持单个对象及IList<> 或 List<>  字典型集合暂不支持
    /// </summary>
    public class InnerValidAttribute : BaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msg">返回的错误信息</param>
        /// <param name="group">分组，用于解决同一个参数的校验方式在不同业务中使用不同规则</param>
        public InnerValidAttribute(string msg = "", string @group = "") : base(msg, @group)
        {
        }

        /// <summary>
        /// 验证参数是否符合要求
        /// </summary>
        /// <param name="value">参数值</param>
        /// <param name="prop">参数类型</param>
        /// <returns>符合要求=true</returns>
        public override bool Validate(object value, PropertyInfo prop)
        {
            return true;
        }
    }
}
