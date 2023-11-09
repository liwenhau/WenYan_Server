namespace WenYan.Service.IBusiness
{
    public class UserMenuDto : BaseTreeModel<UserMenuDto>
    {
        /// <summary>
        /// 路由地址
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 组件路径
        /// </summary>
        public string? Component { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 重定向
        /// </summary>
        public string? Redirect { get; set; }

        /// <summary>
        /// 类型 1 目录 2 菜单 3 按钮
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Svg图标
        /// </summary>
        public string? SvgIcon { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// 是否固定
        /// </summary>
        public bool Affix { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Permission { get; set; }
    }
}
