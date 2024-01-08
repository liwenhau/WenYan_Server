namespace WenYan.Service.Models
{
    public class UserInfoM
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
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
        /// 当前用户所有角色
        /// </summary>
        public List<string> Roles { get; set; }
        /// <summary>
        /// 当前用户权限
        /// </summary>
        public List<string> Permissions { get; set; }
    }
}
