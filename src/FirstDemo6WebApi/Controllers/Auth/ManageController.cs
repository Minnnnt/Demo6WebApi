using FirstDemo6Application.Dtos.InputDtos;
using FirstDemo6Common.Enums;
using FirstDemo6WebApi.Models;
using FirstDemo6WebCore.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstDemo6WebApi.Controllers.Auth
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(APIVersions.v1))]
    public class ManageController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageController(UserManager<IdentityUser> userManager, IConfiguration configuration, JwtHelper jwtHelper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("role/create")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleInputDto model)
        {
            var role = new IdentityRole { Name = model.RoleName};
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok("role create successfully");
            }

            return BadRequest(result.Errors);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("userRole/assignment")]
        public async Task<IActionResult> AssignmentUserRole([FromBody] AssignmentUserRoleInputDto model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LofinInputDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                string tokenString = _jwtHelper.CreateToken(model.Username, null);

                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }
    }
}
