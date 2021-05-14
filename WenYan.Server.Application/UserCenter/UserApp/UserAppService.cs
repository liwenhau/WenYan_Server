
using Furion.DatabaseAccessor;
using Furion.DataEncryption;

using Mapster;

using System;
using System.Threading.Tasks;

using WenYan.Server.Core;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 用户服务
    /// </summary>    
    public class UserAppService:IUserAppService
    {
        private readonly IRepository<User> _userRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            var user = input.Adapt<User>();
            //密码MD5加密
            user.Password = MD5Encryption.Encrypt(input.Password);
            await _userRepository.InsertAsync(user);
            return user.Adapt<UserDto>();
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> GetAsync(Guid id)
        {
            var user= await _userRepository.FindOrDefaultAsync(id);
            return user.Adapt<UserDto>();
        }
    }
}
