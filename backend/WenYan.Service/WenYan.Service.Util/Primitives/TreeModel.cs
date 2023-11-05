namespace WenYan.Service.Util
{
    public interface ITreeModel<T, K>
    where T : ITreeModel<T, K>
    {
        K Id { get; set; }
        K? ParentId { get; set; }
        List<T> Children { get; set; }
    }
    public interface ITreeModel<T> : ITreeModel<T, string>
        where T : ITreeModel<T>
    {
    }
    /// <summary>
    /// 树模型
    /// </summary>
    public class TreeModel
    {
        /// <summary>
        /// 唯一标识Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public string? ParentId { get; set; }

        /// <summary>
        /// 节点深度
        /// </summary>
        public int? Level { get; set; } = 1;

        /// <summary>
        /// 显示的内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 孩子节点
        /// </summary>
        public List<TreeModel> Children { get; set; }
    }
}
