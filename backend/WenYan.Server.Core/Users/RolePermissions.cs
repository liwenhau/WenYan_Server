using Furion.DatabaseAccessor;

using System;

namespace WenYan.Server.Core
{
    /// <summary>
    /// 角色权限中间表
    /// </summary>
    public class RolePermissions:Entity<Guid>
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid PermissionsId { get; set; }

        /// <summary>
        /// 一对一引用
        /// </summary>
        public Permissions Permissions { get; set; }

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