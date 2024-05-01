using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using School.API.Core.Interfaces;
using School.API.Core.Models.AuthUserRequestResponseModel;
using School.API.Core.DbContext;
using School.API.Core.Dtos;
using Microsoft.AspNetCore.Identity;
using School.API.Core.Entities;
using School.API.Common;
using System.Text;

namespace School.API.Core.Services
{
    public class AuthUserService: IAuthUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _applicationDbContext;
        private readonly EmailSettings emailSettings;
        public AuthUserService(IOptions<EmailSettings> options, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            this._userManager = userManager;
            this.emailSettings = options.Value;
            this._applicationDbContext = applicationDbContext;
        }

        public List<string> GetRoles()
        {
            return _applicationDbContext.Roles.Select(x => x.Name ?? "").ToList();
        }

        public async Task<string> SendEmailAsync(MailRequest mailrequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));
            email.Subject = mailrequest.Subject;
            var builder = new BodyBuilder();


            //byte[] fileBytes;
            //if (System.IO.File.Exists("Attachment/dummy.pdf"))
            //{
            //    FileStream file = new FileStream("Attachment/dummy.pdf", FileMode.Open, FileAccess.Read);
            //    using (var ms = new MemoryStream())
            //    {
            //        file.CopyTo(ms);
            //        fileBytes = ms.ToArray();
            //    }
            //    builder.Attachments.Add("attachment.pdf", fileBytes, ContentType.Parse("application/octet-stream"));
            //    builder.Attachments.Add("attachment2.pdf", fileBytes, ContentType.Parse("application/octet-stream"));
            //}

            builder.HtmlBody = mailrequest.Body;
            email.Body = builder.ToMessageBody();

            try
            {
            using var smtp = new SmtpClient();
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Email, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return "Mail sent Successfully";
            }
            catch(Exception ex)
            {
                throw new EntityInvalidException("Mail not send", ex.Message);
            }
        }

        public async Task<string> ResetPassword(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new EntityInvalidException("Failure", "Not Valid username");
            }
            var token = _userManager.GeneratePasswordResetTokenAsync(user);
            var url = $"http://localhost:4200/newPassword?token={token.Result}";
            StringBuilder html = new StringBuilder();
            html.Append($"<h4>Hello {userName}</h4><br/>");
            html.Append($"<p>Please click on below Url to Reset your password<p>");
            html.Append($"<p>URL: {url}&username={userName}");
            MailRequest request = new MailRequest();
            request.ToEmail = user.Email;
            request.Subject = "Reset your Skool UI Password";
            request.Body = html.ToString();
            var result = SendEmailAsync(request);
            return result.Result;
        }

        public List<RegisterDto> UserDetails()
        {
            var users = _applicationDbContext.Users.ToList();
            var result = (from user in _applicationDbContext.Users
                         join userRole in _applicationDbContext.UserRoles on user.Id equals userRole.UserId
                         join role in _applicationDbContext.Roles on userRole.RoleId equals role.Id
                         select new RegisterDto { 
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserName = user.UserName ?? "",
                            Email = user.Email ?? "",
                            Role = role.Name ?? ""
                            }).ToList();
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordRequest changePasswordRequest, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
              throw new EntityInvalidException("Failure", "Not Valid user");

            var identityResult = _userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);
            return identityResult.Result;
        }

        public async Task<string> ResetPasswordWithToken(string token, string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
                throw new EntityInvalidException("Failure", "Not Valid user");
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return "Password Changed sucessfully.";
            }
            else
            {
                throw new EntityInvalidException("Reset Password Issue", result.Errors.ToList()[0].Description);
            }
        }

        public async Task<MeResponse> UserDetail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new EntityInvalidException("Failure", "Not Valid user");

            return new MeResponse()
            {
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName,
               UserName = user.UserName
            };
        }

        public async Task<string> UpdateUser(UpdateUserRequest user, string userId)
        {
            var userInfo = await _userManager.FindByIdAsync(userId);

            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            _applicationDbContext.SaveChanges();
           
            return "User updated Successfully";
        }
    }
}
