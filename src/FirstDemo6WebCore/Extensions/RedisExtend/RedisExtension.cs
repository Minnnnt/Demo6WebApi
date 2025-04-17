using DotNetCore.CAP;
using DotNetCore.CAP.Messages;
using FirstDemo6Models.Bos.RedisBos;
using FirstDemo6WebCore.Extensions.LoggerWraper;
using FirstDemo6WebCore.Extensions.UtilsExtends;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.Extensions.RedisExtend
{
    /// <summary>
    /// 
    /// </summary>
    public static class RedisExtension
    {
        public static volatile ConnectionMultiplexer _redisConnection;
        private static object _redisConnectionLock = new object();
        private static ConfigurationOptions _configOptions;

        /// <summary>
        /// 
        /// </summary>
        public static void AddRedisExt(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var loggerWrapper = serviceProvider.GetRequiredService<ExtendLoggerWrapper>();
            var options = ReadRedisSetting(services, configuration, loggerWrapper);
            _configOptions = options;
            _redisConnection = ConnectionRedis(loggerWrapper);
        }

        private static ConfigurationOptions ReadRedisSetting(IServiceCollection services, IConfiguration configuration, ExtendLoggerWrapper loggerWrapper)
        {
            try
            {
                List<RedisSettings> configs = new List<RedisSettings>();
                configuration.GetSection("RedisSettings").Bind(configs);
                if (configs.Any())
                {
                    ConfigurationOptions options = new ConfigurationOptions
                    {
                        EndPoints =
                        {
                            {
                                configs.FirstOrDefault().Ip,
                                configs.FirstOrDefault().Port
                            }
                        },
                        //ClientName = configs.FirstOrDefault().Name,
                        //ConnectTimeout = configs.FirstOrDefault().Timeout,
                        DefaultDatabase = configs.FirstOrDefault().Db,
                    };
                    return options;
                }
                return null;
            }
            catch (Exception ex)
            {
                loggerWrapper.LogError($"获取Redis配置信息失败：{ex.Message}");
                return null;
            }
        }

        private static ConnectionMultiplexer ConnectionRedis(ExtendLoggerWrapper loggerWrapper)
        {
            if (_redisConnection != null && _redisConnection.IsConnected)
            {
                return _redisConnection; // 已有连接，直接使用
            }
            lock (_redisConnectionLock)
            {
                if (_redisConnection != null)
                {
                    _redisConnection.Dispose(); // 释放，重连
                }
                try
                {
                    _redisConnection = ConnectionMultiplexer.Connect(_configOptions);
                }
                catch (Exception ex)
                {
                    loggerWrapper.LogError($"Redis服务启动失败：{ex.Message}");
                }
            }
            return _redisConnection;
        }
    }
}
