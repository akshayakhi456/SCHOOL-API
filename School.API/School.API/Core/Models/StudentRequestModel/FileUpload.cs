using Microsoft.AspNetCore.Http;
namespace School.API.Core.Models.StudentRequestModel
{
    public class FileUpload
    {
        public IFormFile? file { get; set; }
        public StudentGuardianRequest? studentGuardian { get; set; }
    }
}
