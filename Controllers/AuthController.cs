using System.Threading.Tasks;
using EjercicioProductos.Models;
using EjercicioProductos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioProductos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(IUserService userService, TokenService tokenService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
        {
            User? user = await _userService.ValidateUserAsync(credentials.Username, credentials.Password);
            if (user == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            string token = _tokenService.GenerateToken(credentials, "user");
            return Ok(new { token });
        }

        [AllowAnonymous]
        [HttpPost("anonymous-token")]
        public IActionResult GetAnonymousToken()
        {
            var guestUser = new UserCredentials
            {
                Username = "guest"
            };

            var token = _tokenService.GenerateToken(guestUser, "guest");

            return Ok(new { token });
        }
    }
}
