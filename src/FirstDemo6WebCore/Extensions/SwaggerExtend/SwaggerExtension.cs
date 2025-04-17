using FirstDemo6Common.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Extensions.SwaggerExtend
{
    /// <summary>
    /// swagger 封装扩展
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// 配置swagger
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerExt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            #region Swagger
            services.AddSwaggerGen(options =>
            {
                foreach (var version in typeof(APIVersions).GetEnumNames())
                {
                    options.SwaggerDoc($"{version}", new OpenApiInfo
                    {
                        Version = $"{version}",
                        Title = $"接口文档——Netcore 6.0",
                        Description = $"API描述,{version}版本",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Example Contact",
                            Url = new Uri("https://example.com/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Example License",
                            Url = new Uri("https://example.com/license")
                        }
                    });
                }
                //xml文档的绝对路径
                var file = Path.Combine(AppContext.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml");
                //true：显示控制器曾的注释
                options.IncludeXmlComments(file, true);
                //对action的名称进行排序，如果有多个，效果明显
                options.OrderActionsBy(o => o.RelativePath);

                #region 支持JWT token授权
                //添加安全定义--配置支持token授权机制
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Bearer xxxx",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearar"
                });
                //添加安全要求
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference =new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },new string[]{ }
                    }
                });
                #endregion
            });
            #endregion
        }

        /// <summary>
        /// 使用swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExt(this WebApplication app)
        {
            var swaggerBasePath = "sc/api/v20241127";
            app.UseSwagger();
            app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
            {
                foreach (var version in typeof(APIVersions).GetEnumNames())
                {
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{version}");
                }
                options.RoutePrefix = $"{swaggerBasePath}";
            });
        }
    }
}
