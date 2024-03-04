namespace WenYan.Service.Entity
{
    public class Sys_File : BusEntity
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string? ExtendName { get; set; }
        /// <summary>
        /// 外链地址（方便前端预览）
        /// </summary>
        public string? Src { get; set; }
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string? FilePath { get; set; }
        /// <summary>
        /// 文字大小KB
        /// </summary>
        public string? SizeKb { get; set; }
        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }
        /// <summary>
        /// 是否目录/文件夹
        /// </summary>
        public bool IsDir { get; set; }
        /// <summary>
        /// 所属目录
        /// </summary>
        public string? DirId { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
    }
    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_FileTypeConfig : BusEntityTypeConfig<Sys_File>, IEntityTypeConfiguration<Sys_File>
    {
        public override void Configure(EntityTypeBuilder<Sys_File> builder)
        {
            base.Configure(builder);

            #region 主外键关系
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Name).HasMaxLength(EntityDefaultConf.DefMiddleColLen).IsRequired();
            builder.Property(p => p.ExtendName).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.Type).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            builder.Property(p => p.Src).HasMaxLength(EntityDefaultConf.DefUrlColLen);
            builder.Property(p => p.FilePath).HasMaxLength(EntityDefaultConf.DefUrlColLen);
            builder.Property(p => p.SizeKb).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.FileMd5).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            builder.Property(p => p.IsDir).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.DirId).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            #endregion

            #region 备注
            builder.ToTable(t => t.HasComment("系统文件"));
            builder.Property(p => p.Name).HasComment("文件名称");
            builder.Property(p => p.ExtendName).HasComment("文件扩展名");
            builder.Property(p => p.Type).HasComment("文件类型");
            builder.Property(p => p.Src).HasComment("外链地址");
            builder.Property(p => p.FilePath).HasComment("文件路径");
            builder.Property(p => p.SizeKb).HasComment("文件大小KB");
            builder.Property(p => p.FileMd5).HasComment("文件MD5");
            builder.Property(p => p.IsDir).HasComment("是否目录");
            builder.Property(p => p.DirId).HasComment("上级目录id");
            #endregion

            #region 种子数据
            builder.HasData(new Sys_File()
            { Id = "1", Name = "Files", ExtendName = null, Type = "Dir", Src = null, FilePath = "/", SizeKb = null, IsDir = true, DirId = null, CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            //builder.HasData(new Sys_File()
            //{ Id = "2", Name = "头像1", ExtendName = ".jpg", Type = "Image", Src = "https://avatars.githubusercontent.com/u/43628298?v=4", FilePath = "/Files/avatar01.jpg", SizeKb = "120", IsDir = false, DirId = "1", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #endregion
        }
    }
}
