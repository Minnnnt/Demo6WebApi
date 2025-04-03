using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Dtos.InputDtos
{
    public class AssignmentUserRoleInputDto
    {
        public string UserId { set; get; }

        public string RoleId { set; get; }

        public int Status { set; get; }
    }
}
