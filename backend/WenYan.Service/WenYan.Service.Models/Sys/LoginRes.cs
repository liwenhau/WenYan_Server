namespace WenYan.Service.Models
{
    /// <summary>
    /// 用户登录返回信息
    /// </summary>
    public class LoginRes
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
