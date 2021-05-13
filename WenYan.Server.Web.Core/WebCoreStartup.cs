using Furion;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WenYan.Server.Web.Core
{
    /// <summary>
    /// 启动默认调用的类
    /// </summary>
    [AppStartup(800)]//配置顺序
    public class WebCoreStartup: AppStartup
    {
        //配置应用所需服务
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsAccessor();

            services.AddControllers().AddInject();
        }

        //配置应用请求处理管道
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCorsAccessor();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseInject(string.Empty);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
