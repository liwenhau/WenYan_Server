using Furion.DynamicApiController;

using System.Threading.Tasks;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 授权服务接口
    /// </summary>
    public interface IAuthorizeAppService: IDynamicApiController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input">登录信息</param>
        /// <returns></returns>
        Task<LoginOutput> LoginAsync(LoginInput input);
    }
}
