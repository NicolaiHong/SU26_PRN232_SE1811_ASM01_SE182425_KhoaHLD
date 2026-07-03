using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HEMSystems.Services.KhoaHLD;
using HEMSystems.Services.KhoaHLD.DTOs;
using HEMSystems.WebApp.KhoaHLD.Commons;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITokenBlacklistService _tokenBlacklist;

        public SystemUserAccountsController(
            IConfiguration config,
            ISystemUserAccountService userAccountsService,
            ITokenBlacklistService tokenBlacklist)
        {
            _config = config;
            _userAccountsService = userAccountsService;
            _tokenBlacklist = tokenBlacklist;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] GetUserAccountRequest request)
        {
            var response = await _userAccountsService.GetUserAccount(request);

            if (response == null)
            {
                return Unauthorized();
            }

            var token = GenerateJSONWebToken(response);
            if (string.IsNullOrWhiteSpace(token))
            {
                var apiResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = "This role is not allowed to access the system.",
                    Data = null
                };

                return StatusCode(StatusCodes.Status403Forbidden, apiResponse);
            }

            return Ok(token);
        }

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            if (string.IsNullOrWhiteSpace(jti))
            {
                var badRequestResponse = new ApiResponse<string?>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Token does not contain a valid identifier.",
                    Data = null
                };

                return StatusCode(StatusCodes.Status400BadRequest, badRequestResponse);
            }

            _tokenBlacklist.Revoke(jti, GetTokenExpiryUtc());

            var apiResponse = new ApiResponse<string?>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Logged out successfully",
                Data = null
            };

            return StatusCode(StatusCodes.Status200OK, apiResponse);
        }

        private DateTime GetTokenExpiryUtc()
        {
            var expClaim = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (long.TryParse(expClaim, out var expSeconds))
            {
                return DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
            }

            return DateTime.UtcNow.AddMinutes(120);
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
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Name, systemUserAccount.UserName),
                    new(ClaimTypes.Role, systemUserAccount.RoleId.ToString())
                },
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
