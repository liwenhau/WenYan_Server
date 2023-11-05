namespace WenYan.Service.Util
{
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; } = true;

        /// <summary>
        /// 错误代码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string message { get; set; }
    }

    /// <summary>
    /// Ajax请求结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AjaxResult<T> : AjaxResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
}
