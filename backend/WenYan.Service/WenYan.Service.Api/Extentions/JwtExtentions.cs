﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace WenYan.Service.Api
{
    /// <summary>
    /// JWT扩展
    /// </summary>
    public static class JwtExtentions
    {
        /// <summary>
        /// JWT认证选项
        /// </summary>
        public class JwtOptions
        {
            /// <summary>
            /// 密钥
            /// </summary>
            public string Secret { get; set; }
            /// <summary>
            /// 过期时间（小时）
            /// </summary>
            public int ExpireHours { get; set; }
            /// <summary>
            /// 刷新时间（小时）
            /// </summary>
            public int RefreshHours { get; set; }
        }
        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtOptions>(config.GetSection("JwtAuth"));
            var jwtOption = config.GetSection("JwtAuth").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }
    }
}
