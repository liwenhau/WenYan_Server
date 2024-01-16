namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class UserDetailM
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户名（账号）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 个性签名/简介
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 组织
        /// </summary>
        public string OrgId { get; set; }
        /// <summary>
        /// <summary>
        /// 当前用户所有角色id
        /// </summary>
        public List<string> RoleIds { get; set; }
    }
}
