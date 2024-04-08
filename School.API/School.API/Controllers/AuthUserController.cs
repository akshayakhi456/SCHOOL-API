using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return null;
        }
    }
}
