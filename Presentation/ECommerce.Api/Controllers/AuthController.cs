using ECommerce.Api.Controllers.Base;
using ECommerce.Contract.Services;
using ECommerce.Data.DTO;
using ECommerce.Ground;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _auth.Login(model);
            return GetApiResponse(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _auth.Register(model);
            return Ok(result);
        }
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] UserTokensDTO model)
        {
            var result = await _auth.RefreshToken(model);
            return GetApiResponse(result);
        }
        [HttpGet("user-details")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails()
        {
            var userId = User.GetUserId();
            var userRoles = User.GetUserRoles();
            var result = userRoles.Select(a => new
            {
                RoleName = a.ToString(),
                RoleId = a
            }).ToList();
            return Ok(result);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutDTO model)
        {
            var result = await _auth.Logout(User.GetUserId(), model);
            return GetApiResponse(result);
        }
    }
}
