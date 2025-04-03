using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsClass
    {

        /// <summary>
        /// 所在学院
        /// </summary>
        public string CollegeId { get; set; }

        /// <summary>
        /// 所属专业ID
        /// </summary>
        public string MajorId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 班级人数
        /// </summary>
        public float StudentNumber { get; set; }

        /// <summary>
        /// 辅导员
        /// </summary>
        public string Adviser { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime EstabDate { get; set; }

        /// <summary>
        /// 学习年数
        /// </summary>
        public float YearNumber { get; set; }

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
