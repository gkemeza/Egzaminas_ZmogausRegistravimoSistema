using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egzaminas_ZmogausRegistravimoSistema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("SignUp")]
        public IActionResult SignUp()
        {

        }
    }
}
