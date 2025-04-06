using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading;
using FirstDemo6WebCore.Attributes;
using FirstDemo6Models.Bos.RabbitMQBos;

namespace FirstDemo6WebCore.Extensions.RabbiteMQExtend
{
    public abstract class MyEventHandler<T> : IMyEventHandler<T> where T : class
    {
        private IChannel _channel;
        private string _queueName;
        private AsyncEventingBasicConsumer _consumer;
        public MyEventHandlerOptions Options = new()
        {
            DisableDeserializeObject = false
        };

        public void Begin(IConnection connection)
        {
            var type = typeof(T);
            // 获取类上的QueueNameAttribute特性，如果不存在则使用类的完整名
            var attr = type.GetCustomAttribute<QueueNameAttribute>();
            if (string.IsNullOrWhiteSpace(attr?.QueueName))
            {
                _queueName = string.IsNullOrEmpty(type.FullName) ? "default" : type.FullName;
            }
            else
            {
                _queueName = attr.QueueName;
            }

            //创建通道
            _channel = connection.CreateChannelAsync().Result;

            // 声明一个队列 
            _channel.QueueDeclareAsync(_queueName, true, false, false, null);

            _consumer = new AsyncEventingBasicConsumer(_channel);
            _consumer.ReceivedAsync += MyReceivedHandler;
            //消费者
            _channel.BasicConsumeAsync(_queueName, false, _consumer);
        }

        /// <summary>
        /// 收到消息后
        /// </summary>
        private async Task MyReceivedHandler(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                // 如果未配置禁用则不解析，后面抽象方法的data参数会始终为空
                if (!Options.DisableDeserializeObject)
                {
                    // 反序列化为对象
                    var message = Options.Encoding.GetString(e.Body.ToArray());
                    T data = JsonConvert.DeserializeObject<T>(message);
                    await OnReceivedAsync(data, message);

                    // 确认该消息已被消费
                    await _channel.BasicAckAsync(e.DeliveryTag, false);
                }
            }
            catch (Exception ex)
            {
                OnConsumerException(ex);
            }
        }

        /// <summary>
        /// 收到消息 
        /// </summary>
        /// <param name="data">解析后的对象</param>
        /// <param name="message">消息原文</param> 
        /// <remarks>Options.DisableDeserializeObject为true时，data始终为null</remarks>
        public abstract Task OnReceivedAsync(T data, string message);

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="ex">派生类不重写的话，异常被隐藏</param>
        public virtual void OnConsumerException(Exception ex)
        {
            
        }
    }
}
