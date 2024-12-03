using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Create a user account
        /// </summary>
        /// <returns></returns>
        [HttpPost("SignUp")]
        public IActionResult SignUp()
        {

        }
    }
}
