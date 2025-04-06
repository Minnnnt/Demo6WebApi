using DotNetCore.CAP;
using FirstDemo6Application.Dtos.InputDtos;
using FirstDemo6Application.Services.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Services.Impls.BusinessServices
{
    public class NotificationService : INotificationService
    {
        private readonly ICapPublisher _capPublisher;

        public NotificationService(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        void INotificationService.AcademicAffairsNotice(AcademicAffairsNoticeDto academicAffairsNoticeDto)
        {
            _capPublisher.Publish("your.event.name", academicAffairsNoticeDto);
        }
    }
}
