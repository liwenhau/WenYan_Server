using System.Reflection;

namespace WenYan.Service.Api
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class ServiceExtentions
    {
        /// <summary>
        /// 动态注册所有服务
        /// 约定：Interfaces（注入接口）, IScopedDependency（生命周期），可以有泛型接口，其它不能再继承。
        /// 注意只能注入直接引用的，间接引用的不行
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicinjectionService(this IServiceCollection services)
        {
            //当前程序集
            var entryAssembly = Assembly.GetEntryAssembly();
            //获取当前程序集所引用的外部程序集,不能获取模式分离/间接引用的程序集
            var types = entryAssembly!.GetReferencedAssemblies()
                .Select(Assembly.Load) //装载
                .Concat(new List<Assembly>() { entryAssembly }) //与本程序集合并
                .SelectMany(x => x.GetTypes()) //获取所有类
                .Where(x => !x.IsAbstract && x.IsClass) //排除抽象类
                .Distinct();
            //获取所有继承服务标记的生命周期实现类
            var busTypes = types.Where(x => x.GetInterfaces().Any(t =>
                t == typeof(ITransientDependency)
                || t == typeof(IScopedDependency)
                || t == typeof(ISingletonDependency)
            ));

            foreach (var busType in busTypes)
            {
                //过滤泛型接口
                var busInterface = busType.GetInterfaces()
                    .FirstOrDefault(t => t != typeof(ITransientDependency)
                                         && t != typeof(IScopedDependency)
                                         && t != typeof(ISingletonDependency)
                                         && !t.IsGenericType);

                if (busInterface == null) continue;
                if (typeof(ITransientDependency).IsAssignableFrom(busType))
                    services.AddTransient(busInterface, busType);
                if (typeof(IScopedDependency).IsAssignableFrom(busType))
                    services.AddScoped(busInterface, busType);
                if (typeof(ISingletonDependency).IsAssignableFrom(busType))
                    services.AddSingleton(busInterface, busType);
            }

            return services;
        }

        /// <summary>
        /// 把系统所有Business添加到ServiceCollection
        /// 加载程序集路径动态注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBusService(this IServiceCollection services)
        {
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var busAssembly = Assembly.LoadFrom(Path.Combine(rootPath, "WenYan.Service.Business.dll"));
            var busTypes = busAssembly.GetTypes().Where(w => w.Name.EndsWith("Business")).ToList();
            foreach (var busType in busTypes)
            {
                var busInterface = busType.GetInterfaces().Where(w => w.Name.EndsWith("Business")).FirstOrDefault();
                if (busInterface == null) continue;
                if (typeof(ITransientDependency).IsAssignableFrom(busType))
                    services.AddTransient(busInterface, busType);
                if (typeof(IScopedDependency).IsAssignableFrom(busType))
                    services.AddScoped(busInterface, busType);
                if (typeof(ISingletonDependency).IsAssignableFrom(busType))
                    services.AddSingleton(busInterface, busType);
            }

            return services;
        }
    }
}