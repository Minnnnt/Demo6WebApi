using FirstDemo6Models.Bos.MediatRBos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6WebCore.MediatRs.Requests
{
    /// <summary>
    /// 请求响应模式：通过请求类和处理程序来实现操作逻辑
    /// </summary>
    public class DemoRequest : IRequest<RequestDemoBo>
    {
        public int DemoId { get; set; }
    }
}
