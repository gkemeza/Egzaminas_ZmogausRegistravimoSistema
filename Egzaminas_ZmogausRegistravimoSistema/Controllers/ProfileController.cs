using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Guid _userId;

        public ProfileController(ILogger<ProfileController> logger, IUserRepository userRepository,
            IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _userId = new Guid(_httpContextAccessor!.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpPut("UpdateUsername")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUsername([FromBody] UpdateUsernameRequest req)
        {
            _logger.LogInformation($"Updating username for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.Username = req.NewUsername;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated username for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdatePassword")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordRequest req)
        {
            _logger.LogInformation($"Updating password for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }
            if (!_authService.VerifyPasswordHash(req.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                _logger.LogWarning($"Invalid password for user ID: '{_userId}'");
                return Unauthorized("Current password is incorrect");
            }

            _authService.CreatePasswordHash(req.NewPassword, out var newPasswordHash, out var newPasswordSalt);
            user.PasswordHash = newPasswordHash;
            user.PasswordSalt = newPasswordSalt;

            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated password for user ID: {_userId}");
            return NoContent();
        }
    }
}
