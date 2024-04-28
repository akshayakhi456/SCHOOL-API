namespace School.API.Core.Models.AuthUserRequestResponseModel
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
