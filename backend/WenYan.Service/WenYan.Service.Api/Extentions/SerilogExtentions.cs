using Serilog;

namespace WenYan.Service.Api
{
    /// <summary>
    /// Serilog扩展类
    /// </summary>
    public static class SerilogExtentions
    {
        /// <summary>
        /// 配置Serilog
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHostBuilder AddSerilog(this IHostBuilder host)
        {
            host.UseSerilog((context, service, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(service)
            .Enrich.FromLogContext()
            );
            return host;
        }
    }
}
