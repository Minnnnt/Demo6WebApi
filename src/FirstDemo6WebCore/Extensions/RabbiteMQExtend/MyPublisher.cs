using FirstDemo6Models.Bos.RabbitMQBos;
using FirstDemo6WebCore.Attributes;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace FirstDemo6WebCore.Extensions.RabbiteMQExtend
{
    public class MyPublisher<T> : IMyPublisher<T>, IDisposable where T : class
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string _queueName;
        private readonly RabbitMQSetting _options;

        /// <summary>
        /// 非注入时使用此构造方法
        /// </summary>
        public MyPublisher(IConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 依赖注入自动走这个构造方法
        /// </summary>
        public MyPublisher(IOptionsMonitor<RabbitMQSetting> options, ConnectionFactory factory)
        {
            _options = options.CurrentValue;
            _connection = factory.CreateConnectionAsync().Result;

            //创建通道
            _channel = _connection.CreateChannelAsync().Result;
            //声明一个交换机
            _channel.ExchangeDeclareAsync(_options.ExchangeName, ExchangeType.Direct, true, false, null);

            var type = typeof(T);
            //获取类上的QueneName特性，若不存在则使用完整类名
            var attr = type.GetCustomAttribute<QueueNameAttribute>();
            if (string.IsNullOrEmpty(attr?.QueueName))
            {
                _queueName = string.IsNullOrEmpty(type.FullName) ? "default" : type.FullName;
            }
            else
            {
                _queueName = attr.QueueName;
            }

            //声明一个队列
            _channel.QueueDeclareAsync(_queueName, true, false, false, null);

            //将队列绑定到交换机
            _channel.QueueBindAsync(_queueName, _options.ExchangeName, _queueName, null);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <returns></returns>
        public async Task PublishAsync(T data, Encoding? encoding = null)
        {
            var msg = JsonConvert.SerializeObject(data);
            var body = Encoding.UTF8.GetBytes(msg);
            var properties = new BasicProperties
            {
                Persistent = true // 设置消息持久化
            };
            await _channel.BasicPublishAsync(_options.ExchangeName, _queueName, false, properties, body);
        }

        public void Dispose()
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
            _channel.DisposeAsync();
            _connection.DisposeAsync();
        }
    }
}
