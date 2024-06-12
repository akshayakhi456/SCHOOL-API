using System.Reflection.Metadata;

namespace School.API.Core.Models.StudentMapTeacherRequestResponseModel
{
    public class StudentMapClassResponseModel: StudentMapClassRequestModel
    {
        public string StudentName { get; set; }
        public String Section { get; set; }
    }
}
