using System;

using WenYan.Server.Application.ExtensibleEntityDto;
using WenYan.Server.Core;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserDto: EntityDto<Guid>
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
    }
}
