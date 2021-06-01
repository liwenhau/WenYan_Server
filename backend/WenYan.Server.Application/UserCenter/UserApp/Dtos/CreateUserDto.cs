using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using WenYan.Server.Core;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 新增用户信息
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "账号需在 2 到 32 个字符之间")]
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码（默认MD5加密）
        /// </summary>
        [DisplayName("密码")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "密码需在 5 到 32 个字符之间")]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [DisplayName("头像")]
        public string Avatar { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        public DateTimeOffset? Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        [StringLength(16)]
        public string Phone { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [StringLength(16)]
        public string Tel { get; set; }
    }
}
