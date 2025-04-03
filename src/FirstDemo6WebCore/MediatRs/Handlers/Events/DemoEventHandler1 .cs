using FirstDemo6WebCore.MediatRs.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.MediatRs.Events
{
    /// <summary>
    /// 事件发布 / 订阅模式：发布通知，多个处理程序同时处理同一条通知
    /// </summary>
    public class DemoEventHandler1 : INotificationHandler<DemoEvent>
    {
        public Task Handle(DemoEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Handler 1: Product {notification.Name} (ID: {notification.DemoId}) created.");
            return Task.CompletedTask;
        }
    }
}
