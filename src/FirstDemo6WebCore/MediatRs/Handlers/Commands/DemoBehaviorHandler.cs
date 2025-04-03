using Azure;
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
    /// 管道行为模式：在请求生命周期中插入自定义逻辑，如日志记录、异常处理等
    /// </summary>
    public class DemoBehaviorHandler : IPipelineBehavior<DemoBehaviorHandler, int>
    {
        public async Task<int> Handle(DemoBehaviorHandler request, RequestHandlerDelegate<int> next, CancellationToken cancellationToken)
        {
            // 在请求之前执行的逻辑
            Console.WriteLine($"Handling {typeof(DemoBehaviorHandler).Name}");

            // 调用下一个请求处理程序
            var response = await next();

            // 在响应之后执行的逻辑
            Console.WriteLine($"Handled {typeof(int).Name}");
            return response;
        }
    }
}
