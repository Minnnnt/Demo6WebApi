using Microsoft.EntityFrameworkCore;
using FirstDemo6WebCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using FirstDemo6Domain;
using FirstDemo6WebCore.Helper;
using FirstDemo6WebCore.Middlewares;
using Hangfire.MySql;
using Hangfire;
using System.Reflection;
using FirstDemo6WebCore.Hangfires;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Emit;
using FirstDemo6WebCore.Extensions.CorsExtend;
using FirstDemo6WebCore.Extensions.SwaggerExtend;
using FirstDemo6WebCore.Extensions.CapExtend;
using FirstDemo6Application.Services.BusinessServices;
using FirstDemo6Application.Services.Impls.BusinessServices;
using FirstDemo6Application.Handlers;
using FirstDemo6WebCore.Extensions.ServiceDIExtend;
using FirstDemo6Domain.DomainServices;
using FirstDemo6Application.Services;
using FirstDemo6WebCore.Extensions.RedisExtend;
using FirstDemo6WebCore.Extensions.LoggerWraper;

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
builder.Services.AddCorsExt();
#endregion

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
builder.Services.AddSwaggerExt();
#endregion

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

#region ���Hangfire����
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseStorage(new MySqlStorage(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlStorageOptions
                {
                    TransactionIsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),
                    PrepareSchemaIfNecessary = true,
                    DashboardJobListLimit = 50000,
                    TransactionTimeout = TimeSpan.FromMinutes(1)
                }));
});

// ��� Hangfire ������
builder.Services.AddHangfireServer();

// ע�� HangfireJobService
builder.Services.AddHostedService<TeachingWorkloadStatisticsHangfireJobService>();
#endregion

#region ���MediatR����
// ע�� MediatR ��ָ��Ҫɨ��ĳ��򼯣�ɨ�������г��򼯶�ע����MediatR ����
var refAssembyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
foreach (var asslembyNames in refAssembyNames)
{
    Assembly.Load(asslembyNames);
}
var assemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(assemblies));
#endregion

builder.Services.AddTransient<ExtendLoggerWrapper>();

#region ���cap����
builder.Services.AddMCodeCap(builder.Configuration);
#endregion

#region ���redis����
builder.Services.AddRedisExt(builder.Configuration);
#endregion

//builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<AcademicAffairsNoticeHandler>();
builder.Services.AddServiceDI(typeof(IApplicationService));
//builder.Services.AddServiceDI(typeof(IDomainService));

var app = builder.Build();

//app.UseCors("allCors");
app.UserCorsExt();

// Configure the HTTP request pipeline.��ֻ�п�����������swagger
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
    app.UseSwaggerExt();
    #endregion
}
app.UseRouting();
app.UseHttpsRedirection();
// ʹ�������֤�м��
app.UseAuthentication();
// ʹ����Ȩ�м��
app.UseAuthorization();
// ʹ���Զ����쳣�����м��
app.UseMiddleware<BusinessExceptionMiddleWare>();
// ���� Hangfire �Ǳ���
app.UseHangfireDashboard();

app.MapControllers();

app.Run();
