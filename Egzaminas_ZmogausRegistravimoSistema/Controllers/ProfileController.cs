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

        [HttpPut("UpdateName")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateName([FromBody] UpdateNameRequest req)
        {
            _logger.LogInformation($"Received request to update name for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            if (req.NewFirstName == null && req.NewLastName == null)
            {
                _logger.LogInformation($"No updates were provided for user ID: {_userId}");
                return BadRequest("No updates were provided");
            }
            if (req.NewFirstName != null && string.IsNullOrWhiteSpace(req.NewFirstName))
            {
                _logger.LogWarning("Invalid empty First Name");
                return BadRequest("First name cannot be empty");
            }
            if (req.NewLastName != null && string.IsNullOrWhiteSpace(req.NewLastName))
            {
                _logger.LogWarning("Invalid empty Last Name");
                return BadRequest("Last name cannot be empty");
            }

            if (!string.IsNullOrWhiteSpace(req.NewFirstName))
            {
                user.PersonInfo.FirstName = req.NewFirstName.Trim();
            }
            if (!string.IsNullOrWhiteSpace(req.NewLastName))
            {
                user.PersonInfo.LastName = req.NewLastName.Trim();
            }

            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated name for user ID: {_userId}");
            return NoContent();
        }
    }
}
