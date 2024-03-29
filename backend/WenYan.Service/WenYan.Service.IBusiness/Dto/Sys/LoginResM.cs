﻿namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 用户登录返回信息
    /// </summary>
    public class LoginResM
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
