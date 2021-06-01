using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 登录返回信息
    /// </summary>
    public class LoginOutput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 头像（OSS地址）
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTimeOffset LastLoginTime { get; set; }
    }
}
