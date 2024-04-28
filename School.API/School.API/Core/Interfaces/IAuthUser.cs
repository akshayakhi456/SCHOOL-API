using Microsoft.AspNetCore.Identity;
using School.API.Core.Dtos;
using School.API.Core.Models.AuthUserRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IAuthUser
    {
        Task SendEmailAsync(MailRequest mailrequest);

        List<string> GetRoles();

        List<RegisterDto> UserDetails();

        Task<IdentityResult> ChangePassword(ChangePasswordRequest changePasswordRequest, string userId);
    }
}
