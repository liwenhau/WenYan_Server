using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WenYan.Service.Entity
{
    /// <summary>
    /// 如果需要在设计时以不同于运行时的方式配置 DbContext，
    /// 如果 DbContext 构造函数采用未在 DI 中注册的附加参数，如果你根本不使用 DI，
    /// 或者如果你出于某种原因不希望在 ASP.NET Core 应用程序的 Main 类中使用 CreateHostBuilder 方法
    /// </summary>
    public class GDbContextFactory : IDesignTimeDbContextFactory<GDbContext>
    {
        public GDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            //添加用户机密
            configurationBuilder.AddUserSecrets<GDbContextFactory>();
            //构建配置文件
            var configuration = configurationBuilder.Build();
            //DbContext配置的起始点
            var optionsBuilder = new DbContextOptionsBuilder<GDbContext>();
            var connString = configuration.GetConnectionString("GDbContext");
            Console.WriteLine(connString);
#if SqlServer
            optionsBuilder.UseSqlServer(connString);
#endif
#if MySql
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
#endif
#if Oracle
            optionsBuilder.UseOracle(connString);
#endif
#if Sqlite
            optionsBuilder.UseSqlite(connString);
#endif
            optionsBuilder.EnableSensitiveDataLogging();

            return new GDbContext(optionsBuilder.Options);
        }
    }
    public class GDbContext : DbContext
    {
        public GDbContext(DbContextOptions<GDbContext> options)
  : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //允许将实体类型的配置分解为单独的类，而不是在OnModelCreating(ModelBuilder)中内联
            //把所有实现了IEntityTypeConfiguration的模型配置全部应用到数据库上下文
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
