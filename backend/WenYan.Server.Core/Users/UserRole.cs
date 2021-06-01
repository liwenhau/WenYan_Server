using Furion.DatabaseAccessor;

using System;

namespace WenYan.Server.Core
{
    /// <summary>
    /// 用户角色中间表
    /// </summary>
    public class UserRole : Entity<Guid>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 一对一引用
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 一对一引用
        /// </summary>
        public Role Role { get; set; }
    }
}
