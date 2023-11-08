using WenYan.Service.Util;

namespace WenYan.Service.IBusiness
{
    public interface ISys_UserBusiness: IBusRepository<Sys_User>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">用户登录信息</param>
        /// <returns></returns>
        Task<Sys_User> LoginAsync(LoginM data);

        /// <summary>
        /// 保存刷新Token
        /// </summary>
        /// <param name="data">用户</param>
        /// <param name="refreshToken">刷新Token</param>
        /// <param name="refreshHours">刷新Token过期时间</param>
        /// <returns></returns>
        Task<int> SaveRefreshTokenAsync(Sys_User data,string refreshToken, int refreshHours);

        /// <summary>
        ///  获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<UserModel> GetUserInfoAsync(string userId);
    }
}
