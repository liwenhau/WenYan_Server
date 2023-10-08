namespace WenYan.Service.Entity
{

    /// <summary>
    /// 通用基本模型
    /// </summary>
    /// <typeparam name="K">主键类型</typeparam>
    public class BaseEntity<K>
    {
        /// <summary>
        /// Id,主键
        /// </summary>
        public K Id { get; set; }
    }
    /// <summary>
    /// 通用实体
    /// </summary>
    public class BaseEntity : BaseEntity<string>
    {

    }

    /// <summary>
    /// 默认实体配置
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <typeparam name="K"></typeparam>
    public class BaseEntityTypeConfig<T, K> : IEntityTypeConfiguration<T>
        where T : BaseEntity<K>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            #region 主外键关系
            builder.HasKey(k => k.Id);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值

            #endregion

            #region 备注
            builder.Property(p => p.Id).HasComment("主键");
            #endregion
        }
    }

    public class BaseEntityTypeConfig<T> : BaseEntityTypeConfig<T, string>, IEntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            #region 主外键关系
            builder.HasKey(k => k.Id);
            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Id).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            #endregion

            #region 备注
            builder.Property(p => p.Id).HasComment("主键");
            #endregion
        }

    }

}
