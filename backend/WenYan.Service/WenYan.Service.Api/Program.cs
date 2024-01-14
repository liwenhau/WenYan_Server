using Serilog;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using WenYan.Service.Api;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region ����ע��
//ʹ��Serilog
builder.Host.AddSerilog();
//�Զ�ģ��״̬��֤������Ĭ����Ϊ��
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
//�ڴ滺��
builder.Services.AddMemoryCache();
//�ֲ�ʽ�ڴ滺��
builder.Services.AddDistributedMemoryCache();
//��Ӧ�����м��
builder.Services.AddResponseCaching();
//ѹ����Ӧ�м��
builder.Services.AddResponseCompression();
//ѩ���㷨 ����ע��
builder.Services.AddSingleton<IIdService, IdService>();
//��ǰ�û�
builder.Services.AddScoped<IOperator, Operator>();

builder.Services.AddScoped<ISys_UserBusiness, Sys_UserBusiness>();
builder.Services.AddScoped<ISys_MenuBusiness, Sys_MenuBusiness>();
builder.Services.AddScoped<ISys_RoleBusiness, Sys_RoleBusiness>();
builder.Services.AddScoped<ISys_OrgBusiness, Sys_OrgBusiness>();
builder.Services.AddScoped<ISys_RoleMenuBusiness, Sys_RoleMenuBusiness>();
builder.Services.AddScoped<ISys_UserRoleBusiness, Sys_UserRoleBusiness>();

builder.Services.AddControllers(options =>
{
    //����ȫ������
    var cacheProjileConfig = builder.Configuration.GetSection("CacheProfile").Get<Dictionary<string, CacheProfile>>();
    foreach (var profile in cacheProjileConfig)
    {
        options.CacheProfiles.Add(profile.Key, profile.Value);
    }
    //�رղ���Ϊ�յ���������
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Filters.Add<FormatResponseAttribute>();
})
.AddJsonOptions(options =>
{   //����ĸСд,null�򲻸ı��Сд
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    //����Unicode���벻����ת�壬�����Ҫ��Html��ǩ������ת�� JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    //������������
    options.JsonSerializerOptions.AllowTrailingCommas = false;
});
//swagger
builder.Services.AddSwashbuckle();
//JWT��֤
builder.Services.AddJWT(builder.Configuration);
#endregion
var app = builder.Build();
// Configure the HTTP request pipeline.
#region �м��
//�������
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.DisallowCredentials();
});
//���serilog������־
app.UseSerilogRequestLogging((options) =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContent) =>
    {
        diagnosticContext.Set("IP", httpContent.Connection.RemoteIpAddress);
    };
    //�Զ���������־��Ϣģ��
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms IP {IP}";
});
//������������ʾswaggerҳ��
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
    //������Ա����ҳ��
    app.UseDeveloperExceptionPage();
}
//ѹ����Ӧ�м��
app.UseResponseCompression();
//��Ӧ�����м��
app.UseResponseCaching();

//��֤
app.UseAuthentication();
//��Ȩ
app.UseAuthorization();

app.MapControllers().RequireAuthorization();
#endregion

app.Run();