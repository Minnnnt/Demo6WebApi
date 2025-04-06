using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Extensions.CorsExtend
{
    /// <summary>
    /// 
    /// </summary>
    public static class CorsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsExt(this IServiceCollection services)
        {
            //配置跨域
            services.AddCors(options =>
            {
                //var cors = configuration.GetSection("CorsUrls").GetChildren().Select(p => p.Value);
                //cor.AddPolicy("Cors", policy =>
                //{
                //    policy.WithOrigins(cors.ToArray())//设置允许的请求头
                //    .WithExposedHeaders("x-custom-header")//设置公开的响应头
                //    .AllowAnyHeader()//允许所有请求头
                //    .AllowAnyMethod()//允许任何方法
                //    .AllowCredentials()//允许跨源凭据----服务器必须允许凭据
                //    .SetIsOriginAllowed(_ => true);
                //});
                options.AddPolicy("allCors", corsBuilder =>
                {
                    corsBuilder.AllowAnyHeader()
                               .AllowAnyOrigin()
                               .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UserCorsExt(this WebApplication app)
        {
            //使用
            app.UseCors("allCors");
        }
    }
}
