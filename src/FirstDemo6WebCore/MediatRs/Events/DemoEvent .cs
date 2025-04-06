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
    public class DemoEvent : INotification
    {
        public int DemoId { get; set; }
        public string Name { get; set; }
    }
}
