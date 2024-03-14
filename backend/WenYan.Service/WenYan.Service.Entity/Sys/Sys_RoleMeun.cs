namespace WenYan.Service.Entity
{
    public partial class Sys_RoleMenu
    {
        public string RoleId { get; set; }
        public string MenuId { get; set; }
    }

    public partial class Sys_RoleMenu
    {
        public Sys_Role Role { get; set; }
        public Sys_Menu Menu { get; set; }
    }

    /// <summary>
    /// 实体配置
    /// OnModelCreating
    /// </summary>
    public class Sys_RoleMenuTypeConfig : IEntityTypeConfiguration<Sys_RoleMenu>
    {
        public void Configure(EntityTypeBuilder<Sys_RoleMenu> builder)
        {
            //多对多关系模型
            #region 主外键关系
            //多对多配置联合主键 
            builder.HasKey(t => new { t.MenuId, t.RoleId });
            builder.HasOne(ur => ur.Role).WithMany().HasForeignKey(k => k.RoleId);
            builder.HasOne(ur => ur.Menu).WithMany().HasForeignKey(k => k.MenuId);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.MenuId).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            builder.Property(p => p.RoleId).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            #endregion

            #region 备注
            builder.ToTable(t => t.HasComment("角色菜单"));
            #endregion

            #region 种子数据

            builder.HasData(
                new Sys_RoleMenu() { RoleId = "1", MenuId = "100" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "101" }
            );

            builder.HasData(
               new Sys_RoleMenu() { RoleId = "1", MenuId = "200" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "201" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "2010" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "2011" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "900" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "901" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9010" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9011" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9012" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9013" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "902" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9020" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9021" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9022" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9023" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9024" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "903" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9030" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9031" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9032" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9033" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9034" },
                new Sys_RoleMenu() { RoleId = "1", MenuId = "904" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9040" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9041" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9042" },
                    new Sys_RoleMenu() { RoleId = "1", MenuId = "9043" }
            );
            #endregion
        }
    }
}
