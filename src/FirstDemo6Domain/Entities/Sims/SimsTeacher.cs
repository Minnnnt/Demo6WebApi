using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsTeacher
    {

        /// <summary>
        /// 所在学院ID
        /// </summary>
        public string CollegeId { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// 教师ID
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birth { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string GraduateInstitution { get; set; }

        /// <summary>
        /// 从业年限
        /// </summary>
        public float PracticeYears { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Political { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string Marital { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Intro { get; set; }

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
