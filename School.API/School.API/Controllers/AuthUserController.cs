using Microsoft.AspNetCore.Mvc;
using System.Net;
using School.API.Core.Models.Wrappers;
using School.API.Core.Interfaces;
using School.API.Core.Models.AuthUserRequestResponseModel;
using School.API.Core.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using School.API.Core.Middleware;

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
        [CustomAuthorize]
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
        [CustomAuthorize]
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
        [CustomAuthorize]
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

        [HttpGet]
        [Route("resetPassword/{userName}")]
        public IActionResult ResetPassword(string userName)
        {
            try
            {
                var result = _authUserService.ResetPassword(userName);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "User Detail List", result.Result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message, ex.Message));
            }
        }

        [HttpGet]
        [Route("resetPassword")]
        public IActionResult ResetPasswordWithToken([FromQuery]string token, string username, string password)
        {
            try
            {
                var result = _authUserService.ResetPasswordWithToken(token, username, password);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "User Detail List", result.Result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message, ex.Message));
            }
        }

        [HttpGet]
        [Route("me")]
        [CustomAuthorize]
        public IActionResult CurrentLoggedInUser()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = _authUserService.UserDetail(userId);
                return StatusCode(200, new APIResponse<MeResponse>((int)HttpStatusCode.OK, "User Detail", result.Result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));
            }
        }

        [HttpPost]
        [Route("updateUser")]
        [CustomAuthorize]
        public IActionResult CurrentLoggedInUser(UpdateUserRequest user)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = _authUserService.UpdateUser(user, userId);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "User Detail List", result.Result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));
            }
        }

        [HttpPost]
        [Route("updateRole")]
        [CustomAuthorize]
        public IActionResult UpdateUserRole(UpdatePermissionDto updatePermissionDto)
        {
            try
            {
                var result = _authUserService.updateRole(updatePermissionDto);
                return StatusCode(200, new APIResponse<AuthServiceResponseDto>((int)HttpStatusCode.OK, "User Detail", result.Result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));
            }
        }

    }
}
