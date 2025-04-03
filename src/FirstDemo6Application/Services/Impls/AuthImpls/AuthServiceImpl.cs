using FirstDemo6Application.Dtos.InputDtos;
using FirstDemo6Common.Enums;
using FirstDemo6WebCore.Exceptions;
using FirstDemo6WebCore.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo6Application.Services.Impls.AuthImpls
{
    public class AuthServiceImpl
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;

        public AuthServiceImpl(UserManager<IdentityUser> userManager, IConfiguration configuration, JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> AuthUserLogin(LofinInputDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                string tokenString = _jwtHelper.CreateToken(model.Username, null);
                return tokenString;
            }
            throw new BusinessFriendException(ExceptionEnums.BusinessErrorCode.Param_Is_Null,"");
        }
    }
}
