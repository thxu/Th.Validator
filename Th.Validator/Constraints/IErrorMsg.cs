namespace Th.Validator.Constraints
{
    /// <summary>
    /// 基础错误信息接口
    /// </summary>
    internal interface IErrorMsg
    {
        /// <summary>
        /// 返回的错误消息
        /// </summary>
        string Message { get; set; }
    }
}
