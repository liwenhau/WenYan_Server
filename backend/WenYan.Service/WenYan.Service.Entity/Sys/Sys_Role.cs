namespace WenYan.Service.Entity
{
    public class Sys_Role : BusEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_RoleTypeConfig : BusEntityTypeConfig<Sys_Role>, IEntityTypeConfiguration<Sys_Role>
    {
        public override void Configure(EntityTypeBuilder<Sys_Role> builder)
        {
            base.Configure(builder);

            #region 主外键关系

            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Name).HasMaxLength(EntityDefaultConf.DefMiddleColLen).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            #endregion

            #region 备注
            builder.ToTable(t => t.HasComment("角色"));
            builder.Property(p => p.Name).HasComment("角色名称");
            builder.Property(p => p.Code).HasComment("角色编码");
            #endregion

            #region 种子数据
            builder.HasData(
                new Sys_Role { Id = "1", Name = "系统管理员", Code = "Admin", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId },
                new Sys_Role { Id = "2", Name = "普通用户", Code = "User", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId }
            );
            #endregion
        }
    }
}
