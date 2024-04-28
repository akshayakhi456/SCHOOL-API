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
            return _applicationDbContext.Roles.Select(x => x.Name).ToList();
        }

        public async Task SendEmailAsync(MailRequest mailrequest)
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

            using var smtp = new SmtpClient();
            smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.Email, emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
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
                            UserName = user.UserName,
                            Email = user.Email,
                            Role = role.Name
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
    }
}
