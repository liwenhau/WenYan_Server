using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using static WenYan.Service.Api.JwtExtentions;

namespace WenYan.Service.Api
{
    public class OperatorController : BaseController
    {
        private IServiceProvider SvcProvider { get; set; }
        private IConfiguration Config { get; set; }
        private ISys_UserBusiness UserBus { get; set; }
        public IOperator Operator { get; set; }
        public OperatorController(IServiceProvider svcProvider)
        {
            this.SvcProvider = svcProvider;
            this.Config = this.SvcProvider.GetRequiredService<IConfiguration>();
            this.Operator = this.SvcProvider.GetRequiredService<IOperator>();
            this.UserBus = this.SvcProvider.GetRequiredService<ISys_UserBusiness>();
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserModel> GetUserInfoAsync()
        {
            return await this.UserBus.GetUserInfoAsync(this.Operator.UserId);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">登录参数</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginRes> LoginAsync(LoginM data)
        {
            var user = await this.UserBus.LoginAsync(data);
            var jwtOption = this.Config.GetSection("JwtAuth").Get<JwtOptions>();
            var res = new LoginRes();
            res.AccessToken = CreateToken(user, jwtOption);
            res.RefreshToken = CreateRefreshToken();
            await this.UserBus.SaveRefreshTokenAsync(user, res.RefreshToken, jwtOption.RefreshHours);
            return res;
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginRes> RefreshTokenAsync(LoginRes token)
        {
            var jwtOption = this.Config.GetSection("JwtAuth").Get<JwtOptions>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret));
            //根据过期token获取SecurityToken
            var claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token.AccessToken, new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,//验证密钥
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret)),
                ValidateLifetime = false, //验证token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateIssuer = false,//验证颁发者
                ValidateAudience = false//验证使用者
            }, out SecurityToken validateToken);

            if (validateToken is not JwtSecurityToken jwtSecurityToken ||
                                     !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token不合法！");
            }
            //判断RefreshToken是该用户生成
            var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
            _ = userId ?? throw new SecurityTokenException("过期Token信息不存在！");
            var user = await this.UserBus.GetAsync(x => x.Id == userId);
            if (user is null || user.RefreshToken != token.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new("用户不存在或RefreshToken无效！");
            }
            //生成新的Token和RefreshToken
            var res = new LoginRes();
            res.AccessToken = CreateToken(user, jwtOption);
            res.RefreshToken = CreateRefreshToken();
            await this.UserBus.SaveRefreshTokenAsync(user, res.RefreshToken, jwtOption.RefreshHours);
            return res;
        }

        /// <summary>
        /// 颁发token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(Sys_User user, JwtOptions jwtOption)
        {
            var claims = new[]
            {
                new Claim("userId",user.Id),
                //new Claim(ClaimTypes.Name,user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                string.Empty,//颁发者
                string.Empty,//使用者
                claims,
                expires: DateTime.Now.AddHours(jwtOption.ExpireHours),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        /// <summary>
        /// 生成RefreshToken
        /// </summary>
        /// <returns>RefreshToken</returns>
        private string CreateRefreshToken()
        {
            // 创建一个随机的Token用做Refresh Token
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
