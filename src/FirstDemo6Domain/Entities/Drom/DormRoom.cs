using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Drom
{
    public class DormRoom
    {

        /// <summary>
        /// 所在学院
        /// </summary>
        public string CollegeId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 房间ID
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 宿舍楼ID
        /// </summary>
        public string BuildingId { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNo { get; set; }

        /// <summary>
        /// 床位总数
        /// </summary>
        public float BedSum { get; set; }

        /// <summary>
        /// 已占床位数
        /// </summary>
        public float BedOccupiedTotal { get; set; }

        /// <summary>
        /// 宿舍性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 房间面积
        /// </summary>
        public decimal RoomArea { get; set; }

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
