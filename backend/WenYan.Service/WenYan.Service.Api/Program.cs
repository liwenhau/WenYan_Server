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
//ѩ���㷨 ����ע��
builder.Services.AddSingleton<IIdService, IdService>();
//��ǰ�û�
builder.Services.AddScoped<IOperator, Operator>();

builder.Services.AddScoped<ISys_UserBusiness, Sys_UserBusiness>();

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
})
.AddJsonOptions(options =>
{   //���ִ�Сд
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
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