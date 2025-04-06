using DotNetCore.CAP;
using FirstDemo6Application.Dtos.InputDtos;
using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Handlers
{
    public class AcademicAffairsNoticeHandler : ICapSubscribe
    {
        private readonly ILogger<AcademicAffairsNoticeHandler> _logger;

        public AcademicAffairsNoticeHandler(ILogger<AcademicAffairsNoticeHandler> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("your.event.name")]
        public void Handle(AcademicAffairsNoticeDto eventData)
        {
            _logger.LogInformation("CAP + RabbitMq message handler is okkkk");
        }
    }
}
