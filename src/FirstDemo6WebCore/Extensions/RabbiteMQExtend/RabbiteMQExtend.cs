using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstDemo6Models.Bos.RabbitMQBos;

namespace FirstDemo6WebCore.Extensions.RabbiteMQExtend
{
    public static class RabbiteMQExtend
    {/// <summary>
     /// 初始化消息队列，并添加Publisher到IoC容器
     /// </summary>
     /// <returns></returns>
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            //读取配置文件的配置项
            var optionSection = configuration.GetSection("RabbitMQSetting");

            RabbitMQSetting rabbitMQSetting = new RabbitMQSetting();
            optionSection.Bind(rabbitMQSetting);

            // 加了这行，才可以注入IOptions<MyRabbitMQOptions>或者IOptionsMonitor<MyRabbitMQOptions>
            services.Configure<RabbitMQSetting>(optionSection);

            //加了这行，才可以注入任意类型参数的 IMyPublisher<> 使用
            services.AddTransient(typeof(IMyPublisher<>), typeof(MyPublisher<>));

            //创建工厂对象，配置单例注入
            services.AddSingleton(new ConnectionFactory
            {
                UserName = rabbitMQSetting.UserName,
                Password = rabbitMQSetting.Password,
                HostName = rabbitMQSetting.HostName,
                Port = rabbitMQSetting.Port,
                VirtualHost = rabbitMQSetting.VirtualHost
            });

            return services;
        }

        /// <summary>
        /// IServiceCollection的拓展方法，用于发现自定义的EventHandler并添加到服务容器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types">包含了自定义Handler的类集合，可以使用assembly.GetTypes()</param>
        /// <returns></returns>
        public static IServiceCollection AddMyRabbitMQEventHandlers(this IServiceCollection services, Type[] types)
        {
            var baseType = typeof(IMyEventHandler<>);

            //遍历所有types，将继承自IMyEventHandler的类注册到容器
            foreach (var type in types)
            {
                // baseType可以放type，并且type不是baseType
                if (baseType.IsAssignableFrom(type) && baseType != type)
                {
                    // 瞬态注入配置
                    services.AddTransient(typeof(IMyEventHandler<>), type);
                }
            }

            return services;
        }

        /// <summary>
        /// 给app拓展方法
        /// </summary>
        /// <remarks>
        /// 在IoC容器里获取到所有继承自IMyEvetnHandler的实现类，并开启消费者
        /// </remarks>
        public static IApplicationBuilder UseMyEventHandler(this IApplicationBuilder app)
        {
            var handlers = app.ApplicationServices.GetServices(typeof(IMyEventHandler));
            var factory = app.ApplicationServices.GetService<ConnectionFactory>();

            // 遍历调用自定义的Begin方法
            foreach (var h in handlers)
            {
                var handler = h as IMyEventHandler;
                if (factory != null)
                {
                    handler?.Begin(factory.CreateConnectionAsync().Result);
                }
            }
            return app;
        }
    }
}

