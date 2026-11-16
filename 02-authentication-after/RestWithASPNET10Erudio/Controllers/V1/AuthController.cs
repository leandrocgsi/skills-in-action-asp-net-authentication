using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Services;

namespace RestWithASPNET10Erudio.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserAuthService _userAuthService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ILoginService loginService,
            IUserAuthService userAuthService,
            ILogger<AuthController> logger)
        {
            _loginService = loginService;
            _userAuthService = userAuthService;
            _logger = logger;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult SignIn([FromBody] UserDTO user)
        {
            if (user == null ||
                string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                _logger.LogWarning(
                    "Sign in failed: Missing username or password");
                return BadRequest(
                    "Username and password are required.");
            }

            _logger.LogInformation(
                "Attempting to sign in user: {username}",
                user.Username);

            var token = _loginService.ValidateCredentials(user);

            if (token == null) return Unauthorized();

            _logger.LogInformation(
                "User {username} signed in successfully",
                user.Username);
            return Ok(token);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Refresh([FromBody] TokenDTO tokenDto)
        {
            if (tokenDto == null)
                return BadRequest("Invalid client request!");

            var token = _loginService.ValidateCredentials(tokenDto);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost("revoke")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Revoke()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("Invalid Client Request!");

            var result = _loginService.RevokeToken(username);

            if (!result) return BadRequest("Invalid Client Request!");
            return NoContent();
        }

        [HttpPost("create")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(AccountCredentialsDTO))]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] AccountCredentialsDTO user)
        {
            if (user == null)
                return BadRequest("Invalid client request!");

            var result = _loginService.Create(user);
            return Ok(result);
        }
    }
}
