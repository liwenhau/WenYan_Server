namespace WenYan.Service.Util
{
    /// <summary>
    /// 分页返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> : AjaxResult<List<T>>
    {
        public int pageNo { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }
    }
}
