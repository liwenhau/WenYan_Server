using Serilog;

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

using WenYan.Service.Api;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region ����ע��
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
//ע���ļ�����
builder.Services.AddScoped<ISys_FileBusiness, Sys_FileBusiness>();
builder.Services.AddHostedService<MonitoringSystemService>();
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
    //ͳһ��ʽ������
    options.Filters.Add<FormatResponseAttribute>();
    //ȫ���쳣����
    options.Filters.Add<GloabExceptionFilterAsync>();
})
.AddJsonOptions(options =>
{   //����ĸСд,null�򲻸ı��Сд
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    //����Unicode���벻����ת�壬�����Ҫ��Html��ǩ������ת�� JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    //������������
    options.JsonSerializerOptions.AllowTrailingCommas = false;
    //�Զ������ڸ�ʽת�� ����ʱ����
    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
});
//swagger
builder.Services.AddSwashbuckle();
//JWT��֤
builder.Services.AddJWT(builder.Configuration);
//�ļ�����
builder.Services.AddFileServer(builder.Configuration, builder.Environment);
//����̨Log
builder.Services.PrintLog();
//Serilog
builder.Host.AddSerilog();
//��̬ע�����
builder.Services.AddDynamicinjectionService();
//��ʱ��ϢSignalRע��
builder.Services.AddSignalRSercice();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region �м��
//app.UseException();

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
app.MapHub<ChartHub>("/chart").RequireCors(t =>
                                    t.SetIsOriginAllowed((host) => true)
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials());
app.MapControllers().RequireAuthorization();
#endregion

app.Run();