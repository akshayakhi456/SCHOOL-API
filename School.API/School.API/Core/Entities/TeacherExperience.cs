namespace School.API.Core.Entities
{
    public class TeacherExperience
    {
        public int Id { get; set; }
        public string  EmpId  { get; set; }
        public string SchoolName { get; set; }
        public string StartEndDate { get; set; }
        public string Designation { get; set; }
        public bool Status { get; set; }
    }
}
