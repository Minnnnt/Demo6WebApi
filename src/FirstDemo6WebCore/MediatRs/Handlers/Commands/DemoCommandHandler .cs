using FirstDemo6WebCore.MediatRs.Commands;
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
    public class DemoCommandHandler : IRequestHandler<DemoCommand, int>
    {
        public Task<int> Handle(DemoCommand request, CancellationToken cancellationToken)
        {
            // 模拟创建产品并返回新创建产品的 ID
            var newProductId = new Random().Next(100, 1000);
            return Task.FromResult(newProductId);
        }
    }
}
