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
    public class DemoBehavior : IRequest<int>
    {
        public string Name { get; set; }
    }
}
