namespace WenYan.Service.Entity
{
    /// <summary>
    /// 业务实体基类
    /// </summary>
    /// <typeparam name="K">主键类型</typeparam>
    public class BusEntity<K> : BaseEntity<K>
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public K CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public K ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
    }
    /// <summary>
    /// 业务实体基类
    /// </summary>
    public class BusEntity : BusEntity<string>
    {}
    /// <summary>
    /// 业务实体基类默认配置
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <typeparam name="K">主键类型</typeparam>
    public class BusEntityTypeConfig<T, K> : BaseEntityTypeConfig<T, K>, IEntityTypeConfiguration<T>
    where T : BusEntity<K>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.HasIndex(i => i.Deleted);
            builder.HasQueryFilter(q => q.Deleted == false);//查询自动过滤已经删除的记录

            #region 主外键关系

            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Deleted).HasDefaultValue(false);//把是否删除设置为默认False
            builder.Property(p => p.CreateUserId).HasMaxLength(EntityDefaultConf.DefSmallColLen);//把创建人设置为默认值
            builder.Property(p => p.ModifyUserId).HasMaxLength(EntityDefaultConf.DefSmallColLen);//把修改人设置为默认值
            builder.Property(p => p.CreateTime);//把创建时间设置默认值并在增加的时候更新值
            builder.Property(p => p.ModifyTime);//把修改时间设置默认值并在增加和修改的时候更新值
            #endregion

            #region 备注
            builder.Property(p => p.Deleted).HasComment("是否删除");
            builder.Property(p => p.CreateUserId).HasComment("创建人");
            builder.Property(p => p.CreateTime).HasComment("创建时间");
            builder.Property(p => p.ModifyUserId).HasComment("修改人");
            builder.Property(p => p.ModifyTime).HasComment("修改时间");
            #endregion
        }
    }


    public class BusEntityTypeConfig<T> : BusEntityTypeConfig<T, string>, IEntityTypeConfiguration<T>
    where T : BusEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            #region 主外键关系

            #endregion

            #region 字段属性:最大长度,是否必需,默认值
            builder.Property(p => p.Id).HasMaxLength(EntityDefaultConf.DefSmallColLen);
            #endregion

            #region 备注

            #endregion
        }
    }
}
