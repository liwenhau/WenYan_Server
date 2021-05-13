using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace WenYan.Server.Core
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role: Entity<Guid>,IEntityTypeBuilder<Role>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("Role");
            entityBuilder.Property<string>(nameof(Name)).HasMaxLength(30).IsRequired();
            entityBuilder.Property<string>(nameof(Remark)).HasMaxLength(200);
        }
    }
}
