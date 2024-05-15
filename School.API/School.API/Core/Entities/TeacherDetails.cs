namespace School.API.Core.Entities
{
    public class TeacherDetails
    {
        public int id { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public int PassOutYear { get; set; }
    }
}
