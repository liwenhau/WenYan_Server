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

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<UserModel> GetUserInfoAsync(string userId)
        {
            var user = await this.GetAsync(userId);
            var roleIds = await this.GetQueryable<Sys_UserRole>(true)
                .Where(w => w.UserId == userId)
                .Select(s => s.RoleId)
                .ToListAsync();
            var menuIds = await this.GetQueryable<Sys_RoleMenu>(true)
                .Where(w => roleIds.Contains(w.RoleId))
                .Select(s => s.MenuId)
                .ToListAsync();
            var roles = await this.GetQueryable<Sys_Role>(true)
                .Where(w => roleIds.Contains(w.Id))
                .Select(s => s.Code)
                .ToListAsync();
            var menus = await this.GetQueryable<Sys_Menu>(true)
                .Where(s => !string.IsNullOrEmpty(s.Permission))
                .Where(w => menuIds.Contains(w.Id))
                .ToListAsync();
            var permissions = menus.Select(s => s.Permission).ToList();
            return new UserModel()
            {
                Id = user.Id,
                NickName = user.UserName,
                Sex = user.Sex,
                Sign = user.Sign,
                Avatar = user.Avatar,
                Roles = roles,
                Permissions = permissions
            };
        }
    }
}


