using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using School.API.Common;
using School.API.Core.Models.AuthUserRequestResponseModel;
using MailKit.Net.Smtp;
using School.API.Core.UtilityServices.interfaces;

namespace School.API.Core.UtilityServices
{
    public class MailSend: ISendMail
    {
        private readonly EmailSettings _emailSettings;

        public MailSend(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task<string> SendEmailAsync(MailRequest mailrequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Email);
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
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Email, _emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "Mail sent Successfully";
            }
            catch (Exception ex)
            {
                throw new EntityInvalidException("Mail not send", ex.Message);
            }
        }
    }
}
