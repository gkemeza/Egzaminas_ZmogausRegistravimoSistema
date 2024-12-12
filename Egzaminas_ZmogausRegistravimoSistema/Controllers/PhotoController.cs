using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserMapper _userMapper;
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Guid _userId;

        public PhotoController(ILogger<UserController> logger, IUserMapper userMapper,
            IUserRepository userRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userMapper = userMapper;
            _userRepository = userRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _userId = new Guid(_httpContextAccessor!.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        [HttpPost("UploadPhoto")]
        public IActionResult UploadPhoto([FromForm] IFormFile photo)
        {
            _logger.LogInformation($"Uploading photo for user ID: '{_userId}'");

            if (photo == null || photo.Length == 0)
            {
                _logger.LogWarning("No file uploaded or file is empty.");
                return BadRequest("No file uploaded.");
            }

            var user = _userRepository.GetUserById(_userId);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{_userId}'");
                return NotFound("User not found");
            }

            string newPhotoPath = _photoService.GetPhotoPath(photo, "Uploads/Profile-pictures");
            user.PersonInfo.PhotoPath = newPhotoPath;
            _userRepository.UpdateUser(user);

            _logger.LogInformation($"Successfully uploaded photo for user ID: {_userId}");
            return Ok();
        }
    }
}
