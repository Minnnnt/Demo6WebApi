using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Dtos.InputDtos
{
    public class AcademicAffairsNoticeDto
    {
        public string NoticeMessage { set; get; }

        public DateTime NoticeTime { set; get; }

        public DateTime ExpireTime { set; get; }

        public int NoticeType { set; get; }

        public string NoticeTopic { set; get; }

        public int NoticeLevel { set; get; }

        public bool IsDelayed { set; get; }

        public int DelayedDuration { set; get; }

        public int NoticeScope { set; get; }
    }
}
