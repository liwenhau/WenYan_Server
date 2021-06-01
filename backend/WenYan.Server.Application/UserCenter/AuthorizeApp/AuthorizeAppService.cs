using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.FriendlyException;

using Mapster;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using WenYan.Server.Core;

namespace WenYan.Server.Application.UserCenter
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public class AuthorizeAppService : IAuthorizeAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<User> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public AuthorizeAppService(IRepository<User> userRepository, 
                                   ILogger<User> logger,
                                   IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<LoginOutput> LoginAsync(LoginInput input)
        {
            var output = new LoginOutput();
            if (!string.IsNullOrEmpty(input.Email) || !string.IsNullOrEmpty(input.Account))
            {
                var encryptPassword = MD5Encryption.Encrypt(input.Password);
                var user = await _userRepository.FirstOrDefaultAsync(x => (x.Account.Equals(input.Account) || x.Email.Equals(input.Email))
                                                              && x.Password.Equals(encryptPassword));
                _ = user ?? throw Oops.Oh("用户名或密码错误！");
                // 更新登录时间
                user.LastLoginTime = DateTimeOffset.Now;
                output = user.Adapt<LoginOutput>();
                // 生成 token
                var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
                {
                  { "UserId", user.Id },  //存储Id
                  { "Account",user.Account }, //存储用户名
                });
                // 获取刷新 token
                var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 30);
                // 设置请求报文头
                _httpContextAccessor.HttpContext.Response.Headers["x-access-token"] = refreshToken;
                output.AccessToken = accessToken;
                _logger.LogInformation($"用户{input.Email ?? input.Account}登录,时间：{user.LastLoginTime}");
            }
            return output;
        }
    }
}
