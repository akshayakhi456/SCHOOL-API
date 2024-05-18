namespace School.API.Core.Entities
{
    public class StudentMarks
    {
        public int Id { get; set; }
        public int Sid { get; set; }
        public int rollNo { get; set; }
        public string SName { get; set; }
        public int AcedamicYearId { get; set; }
        public string Subject {  get; set; }
        public string ExamName { get; set; }
        public string Marks { get; set; }
    }
}
