using Furion.DynamicApiController;

using System;
using System.Threading.Tasks;
namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserAppService:IDynamicApiController
    {
        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="input">用户信息</param>
        /// <returns></returns>
        Task<UserDto> CreateAsync(CreateUserDto input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        Task<UserDto> GetAsync(Guid id);
    }
}
