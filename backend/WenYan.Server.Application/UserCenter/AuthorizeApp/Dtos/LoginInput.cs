using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(32, MinimumLength = 2, ErrorMessage = "账号需在 2 到 32 个字符之间")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(32, MinimumLength = 5, ErrorMessage = "密码需在 2 到 32 个字符之间")]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }
    }
}
