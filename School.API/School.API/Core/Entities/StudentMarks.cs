namespace School.API.Core.Entities
{
    public class StudentMarks
    {
        public int Id { get; set; }
        public int Sid { get; set; }
        public int RollNo { get; set; }
        public string SName { get; set; }
        public int AcedamicYearId { get; set; }
        public int SubjectId {  get; set; }
        public int ExamId { get; set; }
        public int Marks { get; set; }
    }
}
