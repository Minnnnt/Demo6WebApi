namespace FirstDemo6WebCore.Attributes
{
    /// <summary>
    /// 定义队列名字，优先级高于类完整名
    /// 直接使用类的FullName为队列名称，不太友好，所以单独加了一个特性，用于更改队列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class QueueNameAttribute : Attribute
    {
        public string QueueName { get; }
        public QueueNameAttribute(string queueName)
        {
            QueueName = queueName;
        }
    }

}
