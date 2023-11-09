namespace WenYan.Service.Util
{
    public class BaseTreeModel<T> where T : class
    {
        /// <summary>
        /// 唯一标识Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        /// 孩子节点
        /// </summary>
        public List<T> Children { get; set; }
    }
}
