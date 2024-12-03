using Egzaminas_ZmogausRegistravimoSistema.Dtos.Requests;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _authService;

        public UserController(ILogger<UserController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Create a user account
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignUp")]
        public IActionResult SignUp(UserRequest req)
        {
            _logger.LogInformation($"Creating account for '{req.Username}'");
        }
    }
}
