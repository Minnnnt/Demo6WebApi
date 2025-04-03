using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Drom
{
    public class DormRoomMember
    {

        /// <summary>
        /// 入住记录ID
        /// </summary>
        public string RoomMemberId { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 房间ID
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// 离开时间
        /// </summary>
        public DateTime LeaveDate { get; set; }

        /// <summary>
        /// 住宿状态
        /// </summary>
        public string CheckStatus { get; set; }

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
