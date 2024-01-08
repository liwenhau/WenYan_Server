namespace WenYan.Service.Entity
{
    /// <summary>
    /// 组织架构
    /// </summary>
    public partial class Sys_Org : BusEntity, ITreeModel<Sys_Org>
    {
        /// <summary>
        /// 上级组织
        /// </summary>
        public string ParentId { get; set; }
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
    }
    public partial class Sys_Org : BusEntity
    {
        /// <summary>
        /// 上级组织
        /// </summary>
        public Sys_Org Parent { get; set; }
        /// <summary>
        /// 下级组织
        /// </summary>
        public List<Sys_Org> Children { get; set; }
    }

    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_OrgTypeConfig : BusEntityTypeConfig<Sys_Org>, IEntityTypeConfiguration<Sys_Org>
    {
        public override void Configure(EntityTypeBuilder<Sys_Org> builder)
        {
            base.Configure(builder);

            #region 主外键关系
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.ParentId)
                .IsRequired(false);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Name).HasMaxLength(EntityDefaultConf.DefMiddleColLen).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            builder.Property(p => p.Remark).HasMaxLength(EntityDefaultConf.DefUrlColLen);
            builder.Property(p => p.Seq).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Status).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            #endregion

            #region 备注
            builder.ToTable(t => t.HasComment("组织架构"));
            builder.Property(p => p.ParentId).HasComment("上级组织");
            builder.Property(p => p.Name).HasComment("名称");
            builder.Property(p => p.Code).HasComment("编码");
            builder.Property(p => p.Remark).HasComment("描述");
            builder.Property(p => p.Seq).HasComment("排序");
            builder.Property(p => p.Status).HasComment("状态");
            #endregion

            #region 种子数据
            builder.HasData(
                new Sys_Org { Id = "1", ParentId = null, Code = "Group", Name = "公司",Status="Enable", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId },
                new Sys_Org { Id = "12", ParentId = "1", Code = "Company", Name = "部门", Status = "Enable", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId },
                new Sys_Org { Id = "121", ParentId = "12", Code = "Dept", Name = "子部门", Status = "Enable", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId }
            );
            #endregion
        }
    }
}
