using FirstDemo6Models.Bos.MediatRBos;
using FirstDemo6WebCore.MediatRs.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.MediatRs.Requests
{
    /// <summary>
    /// 请求 / 响应模式：通过请求类和处理程序来实现操作逻辑
    /// </summary>
    public class DemoRequestHandler : IRequestHandler<DemoRequest, RequestDemoBo>
    {
        public Task<RequestDemoBo> Handle(DemoRequest request, CancellationToken cancellationToken)
        {
            var demo = new RequestDemoBo
            {
                Id = request.DemoId,
                Name = $"demo {request.DemoId}"
            };
            return Task.FromResult(demo);
        }
    }
}
