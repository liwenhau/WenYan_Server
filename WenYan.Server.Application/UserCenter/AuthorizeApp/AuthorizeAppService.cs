using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.FriendlyException;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WenYan.Server.Core;

namespace WenYan.Server.Application.UserCenter.AuthorizeApp
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public class AuthorizeAppService : IAuthorizeAppService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<User> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public AuthorizeAppService(IRepository<User> userRepository, ILogger<User> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            if (!string.IsNullOrEmpty(input.Email) || !string.IsNullOrEmpty(input.Account))
            {
                var encryptPassword = MD5Encryption.Encrypt(input.Password);
                var user = await _userRepository.FirstOrDefaultAsync(x => (x.Account.Equals(input.Account) || x.Email.Equals(input.Email))
                                                              && x.Password.Equals(encryptPassword));
                _ = user ?? throw Oops.Oh("用户信息不存在");
                // 更新登录时间
                user.LastLoginTime = DateTimeOffset.Now;

            }
            return new LoginOutput { };
        }
    }
}
