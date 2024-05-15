using School.API.Core.Entities;

namespace School.API.Core.Models.TeacherRequestResponseModel
{
    public class AddTeacherRequest
    {
        public TeacherDetails TeacherDetails { get; set; }
        public List<TeacherExperience> TeacherExperience { get; set;}
    }
}
