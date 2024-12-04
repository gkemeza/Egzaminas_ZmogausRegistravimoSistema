using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _authService;
        private readonly IUserMapper _userMapper;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IAuthService authService,
            IUserMapper userMapper, IUserRepository userRepository)
        {
            _logger = logger;
            _authService = authService;
            _userMapper = userMapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Create a user account
        /// </summary>
        /// <param name="req">User request</param>
        /// <response code= "500">System error</response>
        [HttpPost("SignUp")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult SignUp(UserRequest req)
        {
            _logger.LogInformation($"Creating account for '{req.Username}'");
            var user = _userMapper.Map(req);
            var userId = _userRepository.CreateUser(user);
            _logger.LogInformation($"Account for '{req.Username}' created with id '{userId}'");
            return Created("", new { id = userId });
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="req">User request</param>
        /// <response code="400">Model validation error</response>
        /// <response code= "500">System error</response>
        [HttpPost("Login")]
        [Produces(MediaTypeNames.Text.Plain)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login(UserRequest req)
        {
            _logger.LogInformation($"Login attempt for '{req.Username}'");

            var user = _userRepository.GetUserByUsername(req.Username!);
            if (user == null)
            {
                _logger.LogWarning($"User '{req.Username}' not found");
                return BadRequest("User not found");
            }

            var isPasswordValid = _authService.VerifyPasswordHash(req.Password, user.PasswordHash, user.PasswordSalt);
            if (!isPasswordValid)
            {
                _logger.LogWarning($"Invalid password for '{req.Username}'");
                return BadRequest("Invalid username or password");
            }

            _logger.LogInformation($"User '{req.Username}' succesfully logged in");
            //add jwt
            return Ok();
        }
    }
}
