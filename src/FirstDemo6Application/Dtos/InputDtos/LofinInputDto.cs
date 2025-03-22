using FirstDemo6Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Dtos.InputDtos
{
    public class LofinInputDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public UserEnum.RoleTypeEnum RoleType { get; set; }
    }
}
