namespace WenYan.Service.Models
{
    public class UserModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
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
