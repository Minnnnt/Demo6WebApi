using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsAdmin
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public string AdminId { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// 管理员用户名
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 管理员密码
        /// </summary>
        public string AdminPass { get; set; }

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
