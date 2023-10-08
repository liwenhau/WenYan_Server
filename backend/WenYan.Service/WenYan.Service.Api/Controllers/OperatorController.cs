using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

using WenYan.Service.IBusiness;

namespace WenYan.Service.Api
{
    public class OperatorController : BaseController
    {
        private IServiceProvider SvcProvider { get; set; }
        private IConfiguration Config { get; set; }
        private ISys_UserBusiness Bus { get; set; }
        public OperatorController(IServiceProvider svcProvider, ISys_UserBusiness bus)
        {
            this.SvcProvider = svcProvider;
            this.Config = this.SvcProvider.GetRequiredService<IConfiguration>();
            this.Bus = bus;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">登录参数</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<AjaxResult<string>> LoginAsync()
        {
            var user = await this.Bus.GetAll();
            var jwtOption = this.Config.GetSection("JwtAuth").Get<JwtExtentions.JwtOptions>();
            var claims = new[]
            {
                new System.Security.Claims.Claim("userId","")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                string.Empty,
                string.Empty,
                claims,
                expires: DateTime.Now.Date.AddDays(1),
                signingCredentials: credentials);
            return Success(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
    }
}
