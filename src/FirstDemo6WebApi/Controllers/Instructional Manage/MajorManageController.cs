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
    public class MajorManageController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="configuration"></param>
        public MajorManageController(UserManager<IdentityUser> userManager, IConfiguration configuration, JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("generalUser/create")]
        public async Task<IActionResult> CreateGeneralUser([FromBody] CreateGeneralUserInputDto model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }

            return BadRequest(result.Errors);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("facultyUser/create")]
        public async Task<IActionResult> CreateFacultyUser([FromBody] CreateFacultyUserInputDto model)
        {
            try
            {
                var user = new IdentityUser { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok("User registered successfully");
                }

                return BadRequest(result.Errors);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
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
