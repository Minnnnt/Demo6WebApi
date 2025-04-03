using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsLesson
    {

        /// <summary>
        /// 课程ID
        /// </summary>
        public string LessonId { get; set; }

        /// <summary>
        /// 课程名
        /// </summary>
        public string LessonName { get; set; }

        /// <summary>
        /// 课程说明
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 学时
        /// </summary>
        public float Hours { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// 租户号
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public float Revision { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }


    }
}
