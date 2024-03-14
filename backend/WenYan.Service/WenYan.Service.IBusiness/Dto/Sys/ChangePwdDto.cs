namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ChangePwdDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id {  get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}
