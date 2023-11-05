using Microsoft.Extensions.DependencyInjection;

namespace WenYan.Service.Business
{
    public class Sys_UserBusiness : BusRepository<Sys_User>, ISys_UserBusiness
    {
        private IServiceProvider SvcProvider { get; set; }
        public Sys_UserBusiness(GDbContext context, IServiceProvider svcProvider)
            : base(context)
        {
            SvcProvider = svcProvider;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Sys_User> LoginAsync(LoginM data)
        {
            var user = await this.GetAsync(x => x.UserName == data.UserName
            && x.Password == data.Password);
            _ = user ?? throw new Exception("用户名不存在或密码错误！");
            // 账号是否被冻结
            if (user.Status == "Disable")
                throw new Exception("用户名已被冻结，请联系管理员！"); ;
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="refreshToken"></param>
        /// <param name="refreshHours"></param>
        /// <returns></returns>
        public async Task<int> SaveRefreshTokenAsync(Sys_User data, string refreshToken, int refreshHours)
        {
            var opSvc = this.SvcProvider.GetRequiredService<IOperator>();
            //更新RefreshToken
            data.RefreshToken = refreshToken;
            data.RefreshTokenExpiryTime = DateTime.Now.AddHours(refreshHours);
            data.ModifyTime = DateTime.Now;
            data.ModifyUserId = opSvc.UserId;
            return await this.UpdateAsync(data);
        }
    }
}


