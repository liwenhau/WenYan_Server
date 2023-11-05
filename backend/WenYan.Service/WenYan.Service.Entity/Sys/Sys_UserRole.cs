namespace WenYan.Service.Entity
{
    /// <summary>
    /// 用户角色中间表
    /// </summary>
    public partial class Sys_UserRole
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
    }
    public partial class Sys_UserRole
    {
        public Sys_User User { get; set; }
        public Sys_Role Role { get; set; }
    }
    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_UserRoleTypeConfig : IEntityTypeConfiguration<Sys_UserRole>
    {
        public void Configure(EntityTypeBuilder<Sys_UserRole> builder)
        {
            //多对多关系模型
            #region 主外键关系
            //多对多配置联合主键 
            builder.HasKey(t => new { t.UserId, t.RoleId });
            builder.HasOne(ur => ur.Role).WithMany().HasForeignKey(k => k.RoleId);
            builder.HasOne(ur => ur.User).WithMany().HasForeignKey(k => k.UserId);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.UserId).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.RoleId).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            #endregion

            #region 备注
            builder.ToTable(t => t.HasComment("用户角色"));
            #endregion

            #region 种子数据
            builder.HasData(
                new Sys_UserRole { UserId = EntityDefaultConf.DefAdminUserId, RoleId = "1" }
            );
            #endregion
        }
    }
}
