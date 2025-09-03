using API.DTOs.Auth;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IJwtService jwtService, ILogger<AuthController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// Autentica al usuario y genera un token JWT.
        /// Se puede usar cualquier usuario/contraseña ya que no hay tabla de usuarios en la base de datos.
        /// </summary>
        /// <param name="loginRequest">Credenciales de usuario.</param>
        /// <returns>Token JWT para autorización.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            var token = _jwtService.GenerateToken(loginRequest.Username);
            var expiry = _jwtService.GetTokenExpiry();

            var response = new LoginResponseDto
            {
                Token = token,
                Expires = expiry,
                TokenType = "Bearer"
            };

            _logger.LogInformation($"Token generado para usuario: {loginRequest.Username}");

            return Ok(response);
        }

        /// <summary>
        /// Endpoint de prueba para verificar que la autenticación JWT funciona.
        /// </summary>
        /// <returns>Información del usuario autenticado.</returns>
        [HttpGet("verify")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(new
            {
                message = "Token válido",
                username = username,
                claims = User.Claims.Select(c => new { c.Type, c.Value }).ToArray()
            });
        }
    }
}
