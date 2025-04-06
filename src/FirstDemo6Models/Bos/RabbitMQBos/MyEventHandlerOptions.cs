using System.Text;

namespace FirstDemo6Models.Bos.RabbitMQBos
{
    /// <summary>
    /// Handler的配置
    /// </summary>
    public class MyEventHandlerOptions
    {
        /// <summary>
        /// 禁用 byte[] 解析
        /// </summary>
        public bool DisableDeserializeObject { get; set; } = false;
        /// <summary>
        /// 配置Encoding
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;
    }
}
