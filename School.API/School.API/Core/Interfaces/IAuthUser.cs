using Microsoft.AspNetCore.Identity;
using School.API.Core.Dtos;
using School.API.Core.Models.AuthUserRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IAuthUser
    {
        Task<string> ResetPassword(string userName);

        Task<string> ResetPasswordWithToken(string token, string username, string password);

        List<string> GetRoles();

        List<RegisterDto> UserDetails();

        Task<string> UpdateUser(UpdateUserRequest user, string userId);

        Task<IdentityResult> ChangePassword(ChangePasswordRequest changePasswordRequest, string userId);
        Task<MeResponse> UserDetail(string userId);
    }
}
