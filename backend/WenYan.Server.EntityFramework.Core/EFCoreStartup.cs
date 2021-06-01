using Furion;
using Furion.DatabaseAccessor;

using Microsoft.Extensions.DependencyInjection;

namespace WenYan.Server.EntityFramework.Core
{
    public class EFCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>(DbProvider.SqlServer);
            }, "WenYan.Server.Database.Migrations");
        }
    }
}
