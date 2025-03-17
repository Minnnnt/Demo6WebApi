using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Dtos.InputDtos
{
    public class RegisterInputDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateAdministratorInputDto
    {
        public string AdminName { get; set; }
        public string AdminPass { get; set; }
    }
}
