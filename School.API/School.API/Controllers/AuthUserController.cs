using Microsoft.AspNetCore.Mvc;
using System.Net;
using School.API.Core.Models.Wrappers;
using School.API.Core.Interfaces;
using School.API.Core.Models.AuthUserRequestResponseModel;
using School.API.Core.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthUser _authUserService;
        public AuthUserController(IAuthUser authUserService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _authUserService = authUserService;
        }
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                // Get the current HttpContext
                HttpContext context = _httpContextAccessor.HttpContext;

                // Check if the user is authenticated
                if (context.User.Identity.IsAuthenticated)
                {
                    // Get the user's claims
                    var claims = context.User.Claims;

                    // You can retrieve specific claim values by their type
                    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var result = _authUserService.ChangePassword(changePasswordRequest, userId);
                    return StatusCode(200, new APIResponse<Task<IdentityResult>>((int)HttpStatusCode.OK, "Success", result));
                }
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Password Change failed"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));
            }
        }

        [HttpGet]
        [Route("Roles")]
        public IActionResult RolesList()
        {
            try
            {
                var roles = _authUserService.GetRoles();
                return StatusCode(200, new APIResponse<List<string>>((int)HttpStatusCode.OK, "Roles List", roles));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));

            }
        }

        [HttpGet]
        [Route("UserDetails")]
        public IActionResult UserDetails()
        {
            try
            {
                var list = _authUserService.UserDetails(); 
                return StatusCode(200, new APIResponse<List<RegisterDto>>((int)HttpStatusCode.OK, "User Detail List", list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));
            }
        }

    }
}
