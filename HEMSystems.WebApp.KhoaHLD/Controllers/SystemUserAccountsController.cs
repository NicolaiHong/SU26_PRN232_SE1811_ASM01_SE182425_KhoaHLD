using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HEMSystems.Services.KhoaHLD;
using HEMSystems.Services.KhoaHLD.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HEMSystems.WebApp.KhoaHLD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserAccountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ISystemUserAccountService _userAccountsService;

        public SystemUserAccountsController(IConfiguration config, ISystemUserAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] GetUserAccountRequest request)
        {
            var response = await _userAccountsService.GetUserAccount(request);

            if (response == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(response);

            return Ok(token);
        }

        private string GenerateJSONWebToken(GetUserAccountResponse systemUserAccount)
        {
            if (systemUserAccount.RoleId == 3)
            {
                return string.Empty;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                new Claim[]
                {
                    new(ClaimTypes.Name, systemUserAccount.UserName),
                    new(ClaimTypes.Role, systemUserAccount.RoleId.ToString())
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
