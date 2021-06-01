using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace WenYan.Server.Core
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : Entity<Guid>, IEntityTypeBuilder<User>
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码（默认MD5加密）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTimeOffset? Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTimeOffset LastLoginTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        //配置用户表
        public void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("User");
            entityBuilder.Property<string>(nameof(Account)).HasMaxLength(20).IsRequired();
            entityBuilder.Property<string>(nameof(Password)).HasMaxLength(50).IsRequired();
            entityBuilder.Property<string>(nameof(Name)).HasMaxLength(20).IsRequired();
            entityBuilder.Property<string>(nameof(Email)).HasMaxLength(20).IsRequired();
            entityBuilder.Property<string>(nameof(Phone)).HasMaxLength(20);
            entityBuilder.Property<string>(nameof(Tel)).HasMaxLength(20);
            entityBuilder.Property<string>(nameof(LastLoginIp)).HasMaxLength(20);
            entityBuilder.HasMany(p => p.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<UserRole>(
                  u => u.HasOne(c => c.Role).WithMany(c => c.UserRoles).HasForeignKey(c => c.RoleId)
                , u => u.HasOne(c => c.User).WithMany(c => c.UserRoles).HasForeignKey(c => c.UserId)
                , u =>
                {
                    u.HasKey(c => new { c.UserId, c.RoleId });
                });
        }
        public User()
        {
            Enabled = true;
            CreatedTime = DateTimeOffset.Now;
            IsDeleted = false;
        }
    }
}
