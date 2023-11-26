namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 菜单树
    /// </summary>
    public class MenuTreeDto : BaseTreeModel<MenuTreeDto>
    {
        /// <summary>
        /// 菜单标题
        /// </summary>
        public string title { get; set; }
    }
}
