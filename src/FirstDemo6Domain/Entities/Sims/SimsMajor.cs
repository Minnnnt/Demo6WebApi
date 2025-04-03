using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsMajor
    {

        /// <summary>
        /// 专业ID
        /// </summary>
        public string MajorId { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { get; set; }

        /// <summary>
        /// 专业简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 开设日期
        /// </summary>
        public DateTime EstabDate { get; set; }

        /// <summary>
        /// 专业介绍
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 学费
        /// </summary>
        public decimal TuitionFee { get; set; }

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
