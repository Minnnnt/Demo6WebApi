using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Domain.Entities.Sims
{
    public class SimsStudent
    {

        /// <summary>
        /// 所在学院ID
        /// </summary>
        public string CollegeId { get; set; }
        public string UserId { get; set; }

        /// <summary>
        /// 所在班级ID
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EngName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }

        /// <summary>
        /// 手机号;11位手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 性别;性别说明
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 月薪
        /// </summary>
        public decimal MonthlySalary { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birth { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public float Avatar { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// 名族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Political { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string Marital { get; set; }

        /// <summary>
        /// 籍贯（省）
        /// </summary>
        public string DomicilePlaceProvince { get; set; }

        /// <summary>
        /// 籍贯（市）
        /// </summary>
        public string DomicilePlaceCity { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string DomicilePlaceAddress { get; set; }

        /// <summary>
        /// 爱好
        /// </summary>
        public string Hobby { get; set; }

        /// <summary>
        /// 简要介绍
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 居住地址
        /// </summary>
        public string PresentAddress { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 入学日期
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

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
