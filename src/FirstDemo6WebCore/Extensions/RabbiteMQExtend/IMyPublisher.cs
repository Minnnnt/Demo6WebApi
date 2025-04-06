using System.Text;

namespace FirstDemo6WebCore.Extensions.RabbiteMQExtend
{
    /// <summary>
    /// 用于注入使用
    /// </summary>
    public interface IMyPublisher<T> where T : class
    {
        Task PublishAsync(T data, Encoding? encoding = null);
    }
}