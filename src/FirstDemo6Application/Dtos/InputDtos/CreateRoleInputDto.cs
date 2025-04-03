using FirstDemo6Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Dtos.InputDtos
{
    public class CreateRoleInputDto
    {
        public string RoleName { set; get; }

        public UserEnum.RoleTypeEnum RoleType { set; get; }
    }
}
