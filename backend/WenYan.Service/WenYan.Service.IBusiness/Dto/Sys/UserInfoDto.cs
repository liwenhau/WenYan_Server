namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 用户信息传输对象
    /// </summary>
    public class UserInfoDto: BaseDto<string>
    {
        /// <summary>
        /// 工号、编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 个人头像
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// 个人签名
        /// </summary>
        public string? Sign { get; set; }

        /// <summary>
        /// 刷新Token过期时间
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Remark { get; set; }
    }
}
