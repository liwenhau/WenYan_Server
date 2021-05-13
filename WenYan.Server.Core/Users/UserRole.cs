using Furion.DatabaseAccessor;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenYan.Server.Core
{
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
