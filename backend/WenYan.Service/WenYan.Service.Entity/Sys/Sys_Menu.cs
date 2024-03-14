namespace WenYan.Service.Entity
{
    public partial class Sys_Menu : BusEntity, ITreeModel<Sys_Menu>
    {
        /// <summary>
        /// 上级菜单
        /// </summary>
        public string? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 菜单类型（Dir 1目录 Menu 2菜单 Btn 3按钮）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// Svg图标
        /// </summary>
        public string? SvgIcon { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string? Component { get; set; }
        /// <summary>
        /// 跳转
        /// </summary>
        public string? Redirect { get; set; }
        /// <summary>
        /// 权限标识
        /// </summary>
        public string? Permission { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 外链链接
        /// </summary>
        public string? OutLink { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHide { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool IsKeepAlive { get; set; }

        /// <summary>
        /// 是否固定
        /// </summary>
        public bool IsAffix { get; set; }
    }
    public partial class Sys_Menu : BusEntity
    {
        /// <summary>
        /// 上级菜单
        /// </summary>
        public Sys_Menu Parent { get; set; }
        /// <summary>
        /// 下级菜单
        /// </summary>
        public List<Sys_Menu> Children { get; set; }
    }
    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_MenuTypeConfig : BusEntityTypeConfig<Sys_Menu>, IEntityTypeConfiguration<Sys_Menu>
    {
        public override void Configure(EntityTypeBuilder<Sys_Menu> builder)
        {
            base.Configure(builder);

            #region 主外键关系
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.ParentId);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.ParentId).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired(false);
            builder.Property(p => p.Name).HasMaxLength(EntityDefaultConf.DefMiddleColLen).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            builder.Property(p => p.Type).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            builder.Property(p => p.Status).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.Icon).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.SvgIcon).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.Path).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.Component).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.Redirect).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.Permission).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.Seq).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.OutLink).HasMaxLength(EntityDefaultConf.DefUrlColLen);
            builder.Property(p => p.IsKeepAlive).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsHide).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsAffix).IsRequired().HasDefaultValue(false);
            #endregion

            #region 备注
            builder.ToTable(t => t.HasComment("菜单"));
            builder.Property(p => p.ParentId).HasComment("上级菜单");
            builder.Property(p => p.Name).HasComment("名称");
            builder.Property(p => p.Code).HasComment("编码");
            builder.Property(p => p.Type).HasComment("菜单类型");
            builder.Property(p => p.Status).HasComment("状态");
            builder.Property(p => p.Icon).HasComment("图标");
            builder.Property(p => p.SvgIcon).HasComment("Svg图标");
            builder.Property(p => p.Path).HasComment("路径");
            builder.Property(p => p.Component).HasComment("组件");
            builder.Property(p => p.Redirect).HasComment("跳转");
            builder.Property(p => p.Permission).HasComment("权限标识");
            builder.Property(p => p.Seq).HasComment("排序");
            builder.Property(p => p.OutLink).HasComment("外链链接");
            builder.Property(p => p.IsKeepAlive).HasComment("是否缓存");
            builder.Property(p => p.IsHide).HasComment("是否隐藏");
            builder.Property(p => p.IsAffix).HasComment("是否固定");
            #endregion

            #region 种子数据

            #region 分析页
            builder.HasData(new Sys_Menu() { Id = "100", ParentId = null, Name = "分析页", Code = "Analyse", Type = "1", Icon = "", SvgIcon = "", Status = "Enable", Path = "/analyse", Component = "Layout", Redirect = "analyse/index", Seq = 2, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "101", ParentId = "100", Name = "分析页", Code = "Analyse_Index", Type = "2", Status = "Enable", Icon = "", SvgIcon = "menu-system", Path = "/analyse/index", Component = "analyse/index", Redirect = null, Seq = 1, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #endregion

            #region 文件管理
            builder.HasData(new Sys_Menu() { Id = "200", ParentId = null, Name = "文件管理", Code = "File", Type = "1", Icon = "", SvgIcon = "", Status = "Enable", Path = "/file", Component = "Layout", Redirect = "file/main/index", Seq = 2, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "201", ParentId = "200", Name = "文件管理", Code = "File_Index", Type = "2", Status = "Enable", Icon = "", SvgIcon = "menu-file", Path = "/file/index", Component = "file/main/index", Redirect = null, Seq = 1, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #region 文件管理按钮
            builder.HasData(new Sys_Menu() { Id = "2010", ParentId = "201", Name = "删除", Code = "Sys_File_Delete", Type = "3", Status = "Enable", Permission = "file:btn:delete", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "2011", ParentId = "201", Name = "上传", Code = "Sys_File_Upload", Type = "3", Status = "Enable", Permission = "file:btn:upload", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #endregion
            #endregion

            #region 系统管理
            builder.HasData(new Sys_Menu() { Id = "900", ParentId = null, Name = "系统管理", Code = "Sys", Type = "1", Status = "Enable", Icon = "", SvgIcon= "menu-system", Path = "/system", Component = "Layout", Redirect = "system/user/index", Seq = 9, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "901", ParentId = "900", Name = "用户管理", Code = "Sys_User", Type = "2", Status = "Enable", Icon = "icon-user", SvgIcon = "", Path = "/system/user", Component = "system/user/index", Redirect = null, Seq = 1, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "902", ParentId = "900", Name = "角色管理", Code = "Sys_Role", Type = "2", Status = "Enable", Icon = "icon-common", SvgIcon = "", Path = "/system/role", Component = "system/role/index", Redirect = null, Seq = 2, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "903", ParentId = "900", Name = "菜单管理", Code = "Sys_Menu", Type = "2", Status = "Enable", Icon = "icon-menu", SvgIcon = "", Path = "/system/menu", Component = "system/menu/index", Redirect = null, Seq = 3, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "904", ParentId = "900", Name = "部门管理", Code = "Sys_Org", Type = "2", Status = "Enable", Icon = "IconMindMapping", SvgIcon = "", Path = "/system/dept", Component = "system/dept/index", Redirect = null, Seq = 4, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #region 系统管理按钮
            //用户管理
            builder.HasData(new Sys_Menu() { Id = "9010", ParentId = "901", Name = "新增", Code = "Sys_User_Add", Type = "3", Status = "Enable", Permission = "user:btn:add", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9011", ParentId = "901", Name = "编辑", Code = "Sys_User_Edit", Type = "3", Status = "Enable", Permission = "user:btn:edit", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9012", ParentId = "901", Name = "删除", Code = "Sys_User_Delete", Type = "3", Status = "Enable", Permission = "user:btn:delete", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9013", ParentId = "901", Name = "查询", Code = "Sys_User_Query", Type = "3", Status = "Enable", Permission = "user:btn:query", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            //角色管理
            builder.HasData(new Sys_Menu() { Id = "9020", ParentId = "902", Name = "新增", Code = "Sys_Role_Add", Type = "3", Status = "Enable", Permission = "role:btn:add", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9021", ParentId = "902", Name = "编辑", Code = "Sys_Role_Edit", Type = "3", Status = "Enable", Permission = "role:btn:edit", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9022", ParentId = "902", Name = "删除", Code = "Sys_Role_Delete", Type = "3", Status = "Enable", Permission = "role:btn:delete", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9023", ParentId = "902", Name = "分配权限", Code = "Sys_Role_Permission", Type = "3", Status = "Enable", Permission = "role:btn:permission", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9024", ParentId = "902", Name = "查询", Code = "Sys_Role_Query", Type = "3", Status = "Enable", Permission = "role:btn:query", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            //菜单管理
            builder.HasData(new Sys_Menu() { Id = "9030", ParentId = "903", Name = "新增", Code = "Sys_Menu_Add", Type = "3", Status = "Enable", Permission = "menu:btn:add", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9031", ParentId = "903", Name = "数据结构", Code = "Sys_Menu_Code", Type = "3", Status = "Enable", Permission = "menu:btn:code", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9032", ParentId = "903", Name = "编辑", Code = "Sys_Menu_Edit", Type = "3", Status = "Enable", Permission = "menu:btn:edit", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9033", ParentId = "903", Name = "删除", Code = "Sys_Menu_Delete", Type = "3", Status = "Enable", Permission = "menu:btn:delete", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9034", ParentId = "903", Name = "查询", Code = "Sys_Menu_Query", Type = "3", Status = "Enable", Permission = "menu:btn:query", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            //部门管理
            builder.HasData(new Sys_Menu() { Id = "9040", ParentId = "904", Name = "新增", Code = "Sys_Org_Add", Type = "3", Status = "Enable", Permission = "org:btn:add", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9041", ParentId = "904", Name = "查询", Code = "Sys_Org_Query", Type = "3", Status = "Enable", Permission = "org:btn:query", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9042", ParentId = "904", Name = "编辑", Code = "Sys_Org_Edit", Type = "3", Status = "Enable", Permission = "org:btn:edit", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            builder.HasData(new Sys_Menu() { Id = "9043", ParentId = "904", Name = "删除", Code = "Sys_Org_Delete", Type = "3", Status = "Enable", Permission = "org:btn:delete", Icon = null, Path = null, Component = null, Redirect = null, Seq = 0, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #endregion


            #endregion

            #endregion
        }
    }
}
