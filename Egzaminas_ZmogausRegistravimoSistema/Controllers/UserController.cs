﻿using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Dtos.Results;
using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IJwtService _jwtService;
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IAuthService authService,
            IUserMapper userMapper, IUserRepository userRepository, IJwtService jwtService,
            IPhotoService photoService, IUserService userService)
        {
            _logger = logger;
            _authService = authService;
            _userMapper = userMapper;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _photoService = photoService;
            _userService = userService;
        }

        /// <summary>
        /// Get person info
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>OkObjectResult with PersonInfoResult value</returns>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(PersonInfoResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserPerson([FromRoute] Guid id)
        {
            _logger.LogInformation($"Getting user with ID: '{id}'");

            if (id == Guid.Empty)
            {
                _logger.LogWarning("Invalid user ID provided.");
                return BadRequest("Invalid ID.");
            }

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                _logger.LogWarning($"User not found with ID: '{id}'");
                return NotFound("User not found");
            }

            var personInfo = _userMapper.Map(user);

            try
            {
                byte[] photoBytes = _photoService.GetPhotoAsByteArray(user.PersonInfo.PhotoPath);
                personInfo.PhotoBytes = photoBytes;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"An error occurred: {ex.Message}");
            }

            return Ok(personInfo);
        }

        /// <summary>
        /// Create a user account
        /// </summary>
        /// <param name="req">SignUp request</param>
        [HttpPost("SignUp")]
        [Consumes("multipart/form-data")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SignUp([FromForm] SignUpRequest req)
        {
            _logger.LogInformation($"Creating account for '{req.Username}'");

            if (req.PersonInfo.Photo == null || req.PersonInfo.Photo.Length == 0)
            {
                _logger.LogWarning("No file uploaded or file is empty.");
                return BadRequest("No file uploaded.");
            }

            try
            {
                var userId = _userService.SignUp(req);

                _logger.LogInformation($"Account for '{req.Username}' created with id '{userId}'");
                return Created("", new { id = userId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning($"Sign-up failed for '{req.Username}': {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (IOException ex)
            {
                _logger.LogError($"File handling error for '{req.Username}': {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the photo.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while signing up '{req.Username}': {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="req">Login request</param>
        [HttpPost("Login")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] LoginRequest req)
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

            var jwt = _jwtService.GetJwtToken(user);
            _logger.LogInformation($"User '{req.Username}' succesfully logged in");
            return Ok(new { Token = jwt });
        }

        /// <summary>
        /// Remove a user. Only for admin.
        /// </summary>
        /// <param name="id">User id</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _logger.LogInformation($"Deleting account with ID: '{id}'");
            if (!_userRepository.UserExists(id))
            {
                _logger.LogInformation($"Account not found with ID: '{id}'");
                return NotFound();
            }

            _userRepository.DeleteUser(id);
            return NoContent();
        }
    }
}
