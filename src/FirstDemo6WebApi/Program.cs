using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography.Xml;
using FirstDemo6WebCore.SwaggerExtend;
using System.Configuration;
using FirstDemo6WebCore.CorsExtend;
using Microsoft.EntityFrameworkCore;
using FirstDemo6WebCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using FirstDemo6Domain;
using FirstDemo6WebCore.Helper;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.AddLog4Net("Configs/log4net.config");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

#region 注册自定义ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    // 继承 MySql
    option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});
#endregion

#region 配置跨域
//builder.Services.AddCors(options =>
//{
//    //var cors = configuration.GetSection("CorsUrls").GetChildren().Select(p => p.Value);
//    //cor.AddPolicy("Cors", policy =>
//    //{
//    //    policy.WithOrigins(cors.ToArray())//设置允许的请求头
//    //    .WithExposedHeaders("x-custom-header")//设置公开的响应头
//    //    .AllowAnyHeader()//允许所有请求头
//    //    .AllowAnyMethod()//允许任何方法
//    //    .AllowCredentials()//允许跨源凭据----服务器必须允许凭据
//    //    .SetIsOriginAllowed(_ => true);
//    //});
//    options.AddPolicy("allCors", corsBuilder =>
//    {
//        corsBuilder.AllowAnyHeader()
//                   .AllowAnyOrigin()
//                   .AllowAnyMethod();
//    });
//});
#endregion
builder.Services.AddCorsExt();

#region Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    foreach (var version in typeof(APIVersions).GetEnumNames())
//    {
//        options.SwaggerDoc($"{version}", new OpenApiInfo
//        {
//            Version = $"{version}",
//            Title = $"接口文档——Netcore 6.0",
//            Description = $"API描述,{version}版本",
//            TermsOfService = new Uri("https://example.com/terms"),
//            Contact = new OpenApiContact
//            {
//                Name = "Example Contact",
//                Url = new Uri("https://example.com/contact")
//            },
//            License = new OpenApiLicense
//            {
//                Name = "Example License",
//                Url = new Uri("https://example.com/license")
//            }
//        });
//    }
//    //xml文档的绝对路径
//    var file = Path.Combine(AppContext.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml");
//    //true：显示控制器曾的注释
//    options.IncludeXmlComments(file, true);
//    //对action的名称进行排序，如果有多个，效果明显
//    options.OrderActionsBy(o => o.RelativePath);

//    #region 支持JWT token授权
//    //添加安全定义--配置支持token授权机制
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Bearer xxxx",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        BearerFormat = "JWT",
//        Scheme = "Bearar"
//    });
//    //添加安全要求
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//                {
//                    {
//                        new OpenApiSecurityScheme{
//                            Reference =new OpenApiReference{
//                                Type = ReferenceType.SecurityScheme,
//                                Id ="Bearer"
//                            }
//                        },new string[]{ }
//                    }
//                });
//    #endregion
//});
#endregion
builder.Services.AddSwaggerExt();

#region 注册 Identity 服务
// 注册 Identity 服务
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region JWT 配置
// 从配置文件中获取 JWT 设置
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// 添加 JWT 身份验证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
  {
      //options.TokenValidationParameters = new TokenValidationParameters
      //{
      //    ValidateIssuer = true,
      //    ValidateAudience = true,
      //    ValidateLifetime = true,
      //    ValidateIssuerSigningKey = true,
      //    ValidIssuer = jwtSettings["Issuer"],
      //    ValidAudience = jwtSettings["Audience"],
      //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
      //    ClockSkew = TimeSpan.Zero  // 默认的 5 分钟偏移时间
      //};
      // 配置令牌验证参数
      options.TokenValidationParameters = new TokenValidationParameters
      {
          // 是否验证令牌的签发者
          ValidateIssuer = true,
          // 是否验证令牌的接收者
          ValidateAudience = true,
          // 是否验证令牌的有效期
          ValidateLifetime = true,
          // 是否验证令牌的签名密钥
          ValidateIssuerSigningKey = true,
          // 有效的签发者，从配置文件中读取
          ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
          // 有效的接收者，从配置文件中读取
          ValidAudience = builder.Configuration["JwtSettings:Audience"],
          // 签发者的签名密钥，从配置文件中读取密钥并转换为字节数组，用于创建对称安全密钥
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
          ClockSkew = TimeSpan.Zero
      };
  });
#region
// 配置 JWT 认证另一种写法
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"]
//    };
//});
#endregion
// 添加授权服务，以便后续可以使用 [Authorize] 特性来保护路由
builder.Services.AddAuthorization();
builder.Services.AddSingleton(new JwtHelper(builder.Configuration));
#endregion

#region 过滤器注入
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResultWrapperFilter>();
});
#endregion


var app = builder.Build();

//app.UseCors("allCors");
app.UserCorsExt();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    #region 使用swagger
    //var swaggerBasePath = "sc/api/v20241127";
    //app.UseSwagger(c =>
    //{
    //    c.RouteTemplate = swaggerBasePath + "/swagger/v1/swagger.json";
    //});
    //app.UseSwagger();
    //app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    //{
    //    foreach (var version in typeof(APIVersions).GetEnumNames())
    //    {
    //        options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{version}");
    //    }
    //    options.RoutePrefix = $"{swaggerBasePath}"; 
    //});
    #endregion
    app.UseSwaggerExt();
}
app.UseRouting();
app.UseHttpsRedirection();
// 使用身份验证中间件
app.UseAuthentication();
// 使用授权中间件
app.UseAuthorization();

app.MapControllers();

app.Run();
