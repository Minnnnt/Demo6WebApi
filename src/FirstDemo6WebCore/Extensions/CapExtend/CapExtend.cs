using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using FirstDemo6Models.Bos.RabbitMQBos;
using DotNetCore.CAP.Messages;
using FirstDemo6WebCore.Extensions.UtilsExtends;

namespace FirstDemo6WebCore.Extensions.CapExtend
{
    /// <summary>
    /// 
    /// </summary>
    public static class CapExtend
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection AddMCodeCap(this IServiceCollection services, IConfiguration configuration)
        {
            // 读取配置文件的配置项
            var rabbitMQOptions = new RabbitMQSetting();
            configuration.GetSection("RabbitMQSetting").Bind(rabbitMQOptions);

            if (rabbitMQOptions == null)
            {
                throw new ArgumentNullException("rabbitmq not config.");
            }

            string connectString = configuration.GetConnectionString("DefaultConnection");


            if (string.IsNullOrEmpty(connectString))
            {
                throw new ArgumentException("cap未设置");
            }

            //services.AddDbContext<CapContext>(options => options.UseMySql(capJson, ServerVersion.AutoDetect(capJson)));

            services.AddCap(x =>
            {
                // 使用RabbitMQ传输
                x.UseRabbitMQ(opt => { opt = rabbitMQOptions; });

                // 使用MySQL持久化
                x.UseMySql(connectString);

                //x.UseEntityFramework<CapContext>();

                // 启用仪表板
                x.UseDashboard();

                // 成功消息的过期时间（秒）
                x.SucceedMessageExpiredAfter = 10 * 24 * 3600;

                x.FailedRetryCount = 5;

                // 失败回调，通过企业微信，短信通知人工干预
                x.FailedThresholdCallback = (e) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var loggerWrapper = serviceProvider.GetRequiredService<CapExtendLoggerWrapper>();
                    if (e.MessageType == MessageType.Publish)
                    {
                        loggerWrapper.LogError("Cap发送消息失败;" + JsonExtension.Serialize(e.Message));
                    }
                    else if (e.MessageType == MessageType.Subscribe)
                    {
                        loggerWrapper.LogError("Cap接收消息失败;" + JsonExtension.Serialize(e.Message));
                    }
                };

            });

            return services;
        }
    }

    // 非静态类用于日志记录
    public class CapExtendLoggerWrapper
    {
        private readonly ILogger<CapExtendLoggerWrapper> _logger;

        public CapExtendLoggerWrapper(ILogger<CapExtendLoggerWrapper> logger)
        {
            _logger = logger;
        }

        public void LogError(string message)
        {
            _logger?.LogError(message);
        }
    }

    
}
