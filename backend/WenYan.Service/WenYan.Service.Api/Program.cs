using Serilog;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using WenYan.Service.Api;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region 服务注册
//自动模型状态验证，禁用默认行为　
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddDbContext<GDbContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("GDbContext");
#if SqlServer
                            options.UseSqlServer(connString);
                            options.LogTo((str) => { Console.WriteLine(str.Replace("[", "").Replace("]", "")); }, minimumLevel: LogLevel.Information);
#endif
#if MySql
    options.UseMySql(connString, ServerVersion.AutoDetect(connString));
    options.LogTo((str) => { Console.WriteLine(str.Replace("`", "")); }, minimumLevel: LogLevel.Information);
#endif
#if Oracle
                            options.UseOracle(connString);
                            options.LogTo((str) => { Console.WriteLine(str.Replace("\"", "")); }, minimumLevel: LogLevel.Information);
#endif
#if Sqlite
    options.UseSqlite(connString);
    options.LogTo((str) => { Console.WriteLine(str); }, minimumLevel: LogLevel.Information);
#endif
    options.EnableSensitiveDataLogging();
});
builder.Services.AddControllers();
//HttpContext
builder.Services.AddHttpContextAccessor();
//内存缓存
builder.Services.AddMemoryCache();
//分布式内存缓存
builder.Services.AddDistributedMemoryCache();
//响应缓存中间件
builder.Services.AddResponseCaching();
//压缩响应中间件
builder.Services.AddResponseCompression();
//注册文件服务
builder.Services.AddScoped<ISys_FileBusiness, Sys_FileBusiness>();
builder.Services.AddHostedService<MonitoringSystemService>();
builder.Services.AddControllers(options =>
{
    //缓存全局配置
    var cacheProjileConfig = builder.Configuration.GetSection("CacheProfile").Get<Dictionary<string, CacheProfile>>();
    foreach (var profile in cacheProjileConfig)
    {
        options.CacheProfiles.Add(profile.Key, profile.Value);
    }
    //关闭不可为空的引用类型
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    //统一格式化返回
    options.Filters.Add<FormatResponseAttribute>();
    //全局异常处理
    options.Filters.Add<GloabExceptionFilterAsync>();
})
.AddJsonOptions(options =>
{   //首字母小写,null则不改变大小写
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    //所有Unicode编码不进行转义，如果需要对Html标签不进行转义 JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    //不允许额外符号
    options.JsonSerializerOptions.AllowTrailingCommas = false;
    //自定义日期格式转换 请求时解析
    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
});
//swagger
builder.Services.AddSwashbuckle();
//JWT认证
builder.Services.AddJWT(builder.Configuration);
//文件服务
builder.Services.AddFileServer(builder.Configuration, builder.Environment);
//控制台Log
builder.Services.PrintLog();
//Serilog
builder.Host.AddSerilog();
//动态注入服务
builder.Services.AddDynamicinjectionService();
//即时消息SignalR注册
builder.Services.AddSignalRSercice();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region 中间件
//app.UseException();

//允许跨域
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.DisallowCredentials();
});

//添加serilog请求日志
app.UseSerilogRequestLogging((options) =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContent) =>
    {
        diagnosticContext.Set("IP", httpContent.Connection.RemoteIpAddress);
    };
    //自定义请求日志消息模板
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms IP {IP}";
});

//生产环境不显示swagger页面
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WenYan.Service API");
        options.DefaultModelsExpandDepth(0);
    });
}

if (app.Environment.IsDevelopment())
{
    //开发人员错误页面
    app.UseDeveloperExceptionPage();
}

//压缩响应中间件
app.UseResponseCompression();

//响应缓存中间件
app.UseResponseCaching();

//认证
app.UseAuthentication();
//授权
app.UseAuthorization();
app.MapHub<ChartHub>("/chart").RequireCors(t =>
                                    t.SetIsOriginAllowed((host) => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
app.MapControllers().RequireAuthorization();
#endregion

app.Run();