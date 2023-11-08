using Microsoft.Extensions.Caching.Distributed;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 当前系统操作员
    /// </summary>
    public class Operator : IOperator
    {
        public string UserId { get; }
        private IServiceProvider ServiceProvider { get; set; }
        private IHttpContextAccessor HttpContextAccessor { get; set; }
        public Operator(IServiceProvider service)
        {
            this.ServiceProvider = service;
            this.HttpContextAccessor = this.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            this.UserId = HttpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(w => w.Type == "userId")?.Value;
            if (this.UserId.IsNullOrEmpty()) this.UserId = EntityDefaultConf.DefAdminUserId;
        }

        public async Task<UserModel> GetCurUser()
        {
            var cacheSvc = this.ServiceProvider.GetRequiredService<IDistributedCache>();
            var cacheKey = $"Operator:{this.UserId}";
            var model = await cacheSvc.GetAsync<UserModel>(cacheKey);
            if (model != null) return model;
            var userSvc = this.ServiceProvider.GetRequiredService<ISys_UserBusiness>();
            var entity = await userSvc.GetAsync(this.UserId);
            if (entity == null) throw new Exception("用户不存在！");
            model = new UserModel()
            {
                Id = entity.Id
            };
            await cacheSvc.SetAsync(cacheKey, model, TimeSpan.FromDays(1));
            return model;
        }
    }
}
