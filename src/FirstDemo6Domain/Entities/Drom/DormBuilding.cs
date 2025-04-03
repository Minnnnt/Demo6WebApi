using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Drom
{
    public class DormBuilding
    {

        /// <summary>
        /// 学院ID
        /// </summary>
        public string CollegeId { get; set; }

        /// <summary>
        /// 宿舍楼ID
        /// </summary>
        public string BuildingId { get; set; }

        /// <summary>
        /// 宿舍楼名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 宿舍楼位置
        /// </summary>
        public string BuildingLocation { get; set; }

        /// <summary>
        /// 宿舍楼层数
        /// </summary>
        public float BuildingFloor { get; set; }

        /// <summary>
        /// 建成年份
        /// </summary>
        public string IssueDate { get; set; }

        /// <summary>
        /// 租户号
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public string Revision { get; set; }

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
