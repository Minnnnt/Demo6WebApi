using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsCollege
    {

        /// <summary>
        /// 学院ID
        /// </summary>
        public string CollegeId { get; set; }

        /// <summary>
        /// 学院名称
        /// </summary>
        public string CollegeName { get; set; }

        /// <summary>
        /// 学院简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 学院介绍
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 专业个数
        /// </summary>
        public float ProfessionNumber { get; set; }

        /// <summary>
        /// 学生人数
        /// </summary>
        public float StudentNumber { get; set; }

        /// <summary>
        /// 院长
        /// </summary>
        public string President { get; set; }

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
