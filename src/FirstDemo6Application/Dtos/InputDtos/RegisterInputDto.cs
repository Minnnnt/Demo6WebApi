using FirstDemo6Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FirstDemo6Common.Enums.UserEnum;

namespace FirstDemo6Application.Dtos.InputDtos
{
    public class CreateUserInputDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 所在学院ID
        /// </summary>
        public string CollegeId { get; set; }

        /// <summary>
        /// 所在专业ID
        /// </summary>
        public string MajorId { get; set; }

        /// <summary>
        /// 所在班级ID
        /// </summary>
        public string ClassId { get; set; }

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
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别;性别说明
        /// </summary>
        public string Gender { get; set; }

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
        /// 宿舍号
        /// </summary>
        public string DormitoryNumber { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public UserEnum.RoleTypeEnum RoleType { get; set; }
    }

    public class CreateGeneralUserInputDto:CreateUserInputDto
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birth { get; set; }

        /// <summary>
        /// 入学日期
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserEnum.UserTypeEnum UserType { get; set; } = UserTypeEnum.Generly;
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    public class CreateFacultyUserInputDto : CreateUserInputDto
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserEnum.UserTypeEnum UserType { get; set; } = UserTypeEnum.Faculty;
    }
}
