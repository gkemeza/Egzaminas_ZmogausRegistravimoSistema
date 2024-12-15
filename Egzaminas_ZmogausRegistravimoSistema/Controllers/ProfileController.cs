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
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Guid _userId;

        public ProfileController(ILogger<ProfileController> logger, IUserRepository userRepository,
            IAuthService authService, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authService = authService;
            _photoService = photoService;
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

        [HttpPut("UpdatePersonalIdNumber")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePersonalIdNumber([FromBody] UpdatePersonalIdNumberRequest req)
        {
            _logger.LogInformation($"Updating personal id number for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.PersonalIdNumber = req.NewPersonalIdNumber;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated personal id number for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdatePhoneNumber")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePhoneNumber([FromBody] UpdatePhoneNumberRequest req)
        {
            _logger.LogInformation($"Updating phone number for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.PhoneNumber = req.NewPhoneNumber;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated phone number for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdateEmail")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmail([FromBody] UpdateEmailRequest req)
        {
            _logger.LogInformation($"Updating email for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.Email = req.NewEmail;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated email for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdatePhoto")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePhoto([FromForm] UpdatePhotoRequest req)
        {
            _logger.LogInformation($"Updating photo for user ID: {_userId}");

            if (req.NewPhoto == null || req.NewPhoto.Length == 0)
            {
                _logger.LogWarning("Photo can't be empty");
                return BadRequest("No photo uploaded.");
            }

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            var newPhotoPath = _photoService.GetPhotoPath(req.NewPhoto, "Uploads/Profile-pictures");
            user.PersonInfo.PhotoPath = newPhotoPath;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated photo for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdateCity")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCity([FromBody] UpdateCityRequest req)
        {
            _logger.LogInformation($"Updating city for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.Residence.City = req.NewCity;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated city for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdateStreet")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateStreet([FromBody] UpdateStreetRequest req)
        {
            _logger.LogInformation($"Updating street for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.Residence.Street = req.NewStreet;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated street for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdateHouseNumber")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateHouseNumber([FromBody] UpdateHouseNumberRequest req)
        {
            _logger.LogInformation($"Updating house number for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.Residence.HouseNumber = req.NewHouseNumber;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated house number for user ID: {_userId}");
            return NoContent();
        }

        [HttpPut("UpdateRoomNumber")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRoomNumber([FromBody] UpdateRoomNumberRequest req)
        {
            _logger.LogInformation($"Updating room number for user ID: {_userId}");

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            user.PersonInfo.Residence.RoomNumber = req.NewRoomNumber;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully updated room number for user ID: {_userId}");
            return NoContent();
        }
    }
}
