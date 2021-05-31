using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace WenYan.Server.Core
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Permissions : Entity<Guid>, IEntityTypeBuilder<Permissions>
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级权限ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 角色权限中间表
        /// </summary>
        public List<RolePermissions> RolePermissions { get; set; }
        public void Configure(EntityTypeBuilder<Permissions> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("Permissions");
            entityBuilder.Property<string>(nameof(Name)).HasMaxLength(30).IsRequired();
            entityBuilder.Property<string>(nameof(Remark)).HasMaxLength(200);
        }
    }
}
