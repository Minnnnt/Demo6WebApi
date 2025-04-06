using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.MediatRs.Commands
{
    /// <summary>
    /// 命令模式：命令模式通常用于执行具有事务性的操作
    /// </summary>
    public class DemoCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
