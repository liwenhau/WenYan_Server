using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

using System;
using System.Text;

namespace WenYan.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Inject()
                    .UseStartup<Startup>()
                    //��־
                    .UseSerilogDefault(config =>
                    {
                        string date = DateTime.Now.ToString("yyyy-MM-dd");//��ʱ�䴴���ļ���
                        config.WriteTo.File($"_log/{date}/application.log", 
                            restrictedToMinimumLevel: LogEventLevel.Information,
                            rollingInterval: RollingInterval.Day,
                            encoding: Encoding.UTF8);
                    });
                });
    }
}
