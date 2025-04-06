using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Text;

namespace FirstDemo6WebCore.Extensions.RabbiteMQExtend
{
    /// <summary>
    /// 用于注入使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMyEventHandler<T> : IMyEventHandler where T : class
    {
        Task OnReceivedAsync(T data, string message);
    }

    /// <summary>
    /// 方便程序寻找IMyEventHandler的实现
    /// </summary>
    public interface IMyEventHandler
    {
        void Begin(IConnection connection);
    }
}
