using School.API.Core.Entities;

namespace School.API.Core.Models.StudentRequestModel
{
    public class StudentGuardianRequest
    {
        public Students students { get; set; }
        public List<Guardian> guardians { get; set; }
        public StudentAddress address { get; set; }
    }
}
