using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Common.Enums
{
    public class UserEnum
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        public enum RoleTypeEnum
        {

            Super_Admin,
            Leadership,
            Registry_Staff,
            Teacher,
            Student,
            Headteacher,
            Logisticians,            
            Guest
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum OperationTypeEnum
        {
            Query,
            Add,
            Update,
            Delete
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public enum UserTypeEnum
        {
            Faculty,
            Generly
        }
    }
}
