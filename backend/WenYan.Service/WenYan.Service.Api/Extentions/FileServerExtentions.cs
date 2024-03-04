using Microsoft.Extensions.FileProviders;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 文件服务扩展
    /// </summary>
    public static class FileServerExtentions
    {
        public static IServiceCollection AddFileServer(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.Configure<FileConfig>(config.GetSection("FileConfig"));
            var filecnf = config.GetSection("FileConfig").Get<FileConfig>();
            if (filecnf == null)
                throw new Exception("没有文件服务配置！");
            if (filecnf.RootDir.IsNullOrEmpty())
                throw new Exception("没有设置文件根目录！");
            var rootFilePath = Path.Combine(env.ContentRootPath, filecnf.RootDir);
            if (!Directory.Exists(rootFilePath))
                Directory.CreateDirectory(rootFilePath);
            IFileProvider physicalProvider = new PhysicalFileProvider(rootFilePath);
            services.AddSingleton(physicalProvider);
            return services;
        }
    }
}
