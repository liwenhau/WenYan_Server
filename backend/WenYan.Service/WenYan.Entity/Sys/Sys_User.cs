namespace WenYan.Service.Entity
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public partial class Sys_User : BusEntity
    {
        /// <summary>
        /// 工号、编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 所属组织
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 个人头像
        /// </summary>
        public string HeadPortrait { get; set; }
        /// <summary>
        /// 个人签名
        /// </summary>
        public string Sign { get; set; }

    }
    public partial class Sys_User : BusEntity
    {
        /// <summary>
        /// 所属组织
        /// </summary>
        public Sys_Org Org { get; set; }
    }
    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_UserTypeConfig : BusEntityTypeConfig<Sys_User>, IEntityTypeConfiguration<Sys_User>
    {
        public override void Configure(EntityTypeBuilder<Sys_User> builder)
        {
            base.Configure(builder);

            #region 主外键关系
            builder.HasOne(p => p.Org).WithMany().HasForeignKey(p => p.OrgId);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Code).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.Name).HasMaxLength(EntityDefaultConf.DefMiddleColLen).IsRequired();
            builder.Property(p => p.UserName).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(EntityDefaultConf.DefMiddleColLen).IsRequired();
            builder.Property(p => p.Status).HasMaxLength(EntityDefaultConf.DefSmallColLen).IsRequired();
            builder.Property(p => p.OrgId).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.Sex).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.HeadPortrait).HasMaxLength(EntityDefaultConf.DefLargeColLen);
            builder.Property(p => p.Sign).HasMaxLength(EntityDefaultConf.DefMiddleColLen);
            #endregion

            #region 备注
            builder.HasComment("系统用户");
            builder.Property(p => p.Code).HasComment("编码");
            builder.Property(p => p.Name).HasComment("名称");
            builder.Property(p => p.UserName).HasComment("用户名");
            builder.Property(p => p.Password).HasComment("密码");
            builder.Property(p => p.Status).HasComment("状态");
            builder.Property(p => p.OrgId).HasComment("所属组织");
            builder.Property(p => p.Sex).HasComment("性别");
            builder.Property(p => p.HeadPortrait).HasComment("头像");
            builder.Property(p => p.Sign).HasComment("个人签名");
            #endregion

            #region 种子数据
            builder.HasData(new Sys_User { Id = EntityDefaultConf.DefAdminUserId, Code = "U0000", Name = "Admin", UserName = "admin", Password = $"WenYan@{DateTime.Now.Year}".ToMD5String(), Status = "Enable", OrgId = "1", Sex = "Boy", HeadPortrait="", Sign="后台管理", CreateUserId = EntityDefaultConf.DefAdminUserId, ModifyUserId = EntityDefaultConf.DefAdminUserId });
            #endregion
        }
    }
}
