namespace School.API.Core.Entities
{
    public class ClassAssignSubjectTeacher
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string Subject { get; set; }
        public string SubjectTeacherId { get; set; }
        public bool IsClassTeacher { get; set; }
    }
}
