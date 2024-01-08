namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 组织信息树
    /// </summary>
    public class OrgTreeDto : BaseTreeModel<OrgTreeDto>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
