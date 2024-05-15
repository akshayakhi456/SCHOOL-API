using School.API.Core.Models.AuthUserRequestResponseModel;

namespace School.API.Core.UtilityServices.interfaces
{
    public interface ISendMail
    {
        Task<string> SendEmailAsync(MailRequest mailrequest);
    }
}
