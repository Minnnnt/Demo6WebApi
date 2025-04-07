using FirstDemo6Application.Dtos.InputDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Services.BusinessServices
{
    public interface INotificationService: IApplicationService
    {
        /// <summary>
        /// 教务通知
        /// </summary>
        void AcademicAffairsNotice(AcademicAffairsNoticeDto academicAffairsNoticeDto);
    }
}
