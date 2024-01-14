using Serilog;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using WenYan.Service.Api;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region 服务注册
//使用Serilog
builder.Host.AddSerilog();
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
//雪花算法 单例注入
builder.Services.AddSingleton<IIdService, IdService>();
//当前用户
builder.Services.AddScoped<IOperator, Operator>();

builder.Services.AddScoped<ISys_UserBusiness, Sys_UserBusiness>();
builder.Services.AddScoped<ISys_MenuBusiness, Sys_MenuBusiness>();
builder.Services.AddScoped<ISys_RoleBusiness, Sys_RoleBusiness>();
builder.Services.AddScoped<ISys_OrgBusiness, Sys_OrgBusiness>();
builder.Services.AddScoped<ISys_RoleMenuBusiness, Sys_RoleMenuBusiness>();
builder.Services.AddScoped<ISys_UserRoleBusiness, Sys_UserRoleBusiness>();

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
    options.Filters.Add<FormatResponseAttribute>();
})
.AddJsonOptions(options =>
{   //首字母小写,null则不改变大小写
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    //所有Unicode编码不进行转义，如果需要对Html标签不进行转义 JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    //不允许额外符号
    options.JsonSerializerOptions.AllowTrailingCommas = false;
});
//swagger
builder.Services.AddSwashbuckle();
//JWT认证
builder.Services.AddJWT(builder.Configuration);
#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
#region 中间件
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
    PrintLogToAscll.PrintLog();
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

app.MapControllers().RequireAuthorization();
#endregion

app.Run();