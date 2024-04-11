using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Services;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Username,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (model.Roles != null && model.Roles.Any())
                {
                    result = await _userManager.AddToRolesAsync(user, model.Roles);
                    if (result.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Bad Request");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var checkUser = await _userManager.FindByEmailAsync(model.Username);
            if (checkUser != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(checkUser, model.Password);
                if (checkPassword)
                {
                    var roles = await _userManager.GetRolesAsync(checkUser);
                    if (roles != null && roles.Any())
                    {
                        var jwtToken = _tokenService.CraeteJwtToken(checkUser, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                }
            }
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Username or password incorrect");
        }
    }
}
