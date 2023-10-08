using WenYan.Service.Util;

namespace WenYan.Service.IBusiness
{
    public interface ISys_UserBusiness: IBaseRepository<Sys_User>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">用户登录信息</param>
        /// <returns></returns>
        Task<Sys_User> LoginAsync(LoginM data);
    }
}
