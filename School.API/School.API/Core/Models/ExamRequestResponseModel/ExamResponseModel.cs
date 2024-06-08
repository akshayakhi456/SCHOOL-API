namespace School.API.Core.Models.ExamRequestResponseModel
{
    public class ExamResponseModel
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ClassId { get; set; }
        public int ExamId { get; set; }
        public string MinMarks { get; set; }
        public string MaxMarks { get; set; }
        public bool IsAddInTotal { get; set; }
        public bool WillExamConduct { get; set; }
        public DateTime ExamDate { get; set; }
        public int AcademicYearId { get; set; }
    }
}
