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

#region ע���Զ���ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    // �̳� MySql
    option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
});
#endregion

#region ���ÿ���
//builder.Services.AddCors(options =>
//{
//    //var cors = configuration.GetSection("CorsUrls").GetChildren().Select(p => p.Value);
//    //cor.AddPolicy("Cors", policy =>
//    //{
//    //    policy.WithOrigins(cors.ToArray())//�������������ͷ
//    //    .WithExposedHeaders("x-custom-header")//���ù�������Ӧͷ
//    //    .AllowAnyHeader()//������������ͷ
//    //    .AllowAnyMethod()//�����κη���
//    //    .AllowCredentials()//�����Դƾ��----��������������ƾ��
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
//            Title = $"�ӿ��ĵ�����Netcore 6.0",
//            Description = $"API����,{version}�汾",
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
//    //xml�ĵ��ľ���·��
//    var file = Path.Combine(AppContext.BaseDirectory, $"{AppDomain.CurrentDomain.FriendlyName}.xml");
//    //true����ʾ����������ע��
//    options.IncludeXmlComments(file, true);
//    //��action�����ƽ�����������ж����Ч������
//    options.OrderActionsBy(o => o.RelativePath);

//    #region ֧��JWT token��Ȩ
//    //��Ӱ�ȫ����--����֧��token��Ȩ����
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "Bearer xxxx",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        BearerFormat = "JWT",
//        Scheme = "Bearar"
//    });
//    //��Ӱ�ȫҪ��
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

#region ע�� Identity ����
// ע�� Identity ����
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region JWT ����
// �������ļ��л�ȡ JWT ����
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// ��� JWT �����֤����
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
      //    ClockSkew = TimeSpan.Zero  // Ĭ�ϵ� 5 ����ƫ��ʱ��
      //};
      // ����������֤����
      options.TokenValidationParameters = new TokenValidationParameters
      {
          // �Ƿ���֤���Ƶ�ǩ����
          ValidateIssuer = true,
          // �Ƿ���֤���ƵĽ�����
          ValidateAudience = true,
          // �Ƿ���֤���Ƶ���Ч��
          ValidateLifetime = true,
          // �Ƿ���֤���Ƶ�ǩ����Կ
          ValidateIssuerSigningKey = true,
          // ��Ч��ǩ���ߣ��������ļ��ж�ȡ
          ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
          // ��Ч�Ľ����ߣ��������ļ��ж�ȡ
          ValidAudience = builder.Configuration["JwtSettings:Audience"],
          // ǩ���ߵ�ǩ����Կ���������ļ��ж�ȡ��Կ��ת��Ϊ�ֽ����飬���ڴ����Գư�ȫ��Կ
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
          ClockSkew = TimeSpan.Zero
      };
  });
#region
// ���� JWT ��֤��һ��д��
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
// �����Ȩ�����Ա��������ʹ�� [Authorize] ����������·��
builder.Services.AddAuthorization();
builder.Services.AddSingleton(new JwtHelper(builder.Configuration));
#endregion

#region ������ע��
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
    #region ʹ��swagger
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
// ʹ�������֤�м��
app.UseAuthentication();
// ʹ����Ȩ�м��
app.UseAuthorization();

app.MapControllers();

app.Run();
