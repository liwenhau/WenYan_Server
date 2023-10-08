using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace WenYan.Service.Api
{
    /// <summary>
    /// swagger扩展
    /// </summary>
    public static class SwaggerExtentions
    {
        public static IServiceCollection AddSwashbuckle(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WenYan.Service API",
                    Version = "v1",
                });
                options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "JWT Bearer认证"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });
                var listFile = new List<string>() {
                    "WenYan.Service.Util","WenYan.Service.Entity","WenYan.Service.Api"
                };
                var listPath = listFile.Select(s => Path.Combine(AppContext.BaseDirectory, $"{s}.xml"));
                foreach (var file in listPath)
                {
                    if (File.Exists(file))
                        options.IncludeXmlComments(file);
                }
            });

            return services;
        }
    }
}
