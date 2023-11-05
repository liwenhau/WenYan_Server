namespace WenYan.Service.Util
{
    /// <summary>
    /// 分页查询基类
    /// </summary>
    public class PageInput
    {
        private string _sortOrder { get; set; } = "asc";

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; } = 1000;

        /// <summary>
        /// 排序列
        /// </summary>
        public string SortField { get; set; } = "Id";

        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortOrder { get => _sortOrder; set => _sortOrder = (value ?? string.Empty).ToLower().Contains("desc") ? "desc" : "asc"; }
    }

    /// <summary>
    /// 分页查询基类
    /// </summary>
    /// <typeparam name="Q">查询类型</typeparam>
    public class PageInput<Q> : PageInput where Q : new()
    {
        public Q Search { get; set; }
    }
}
