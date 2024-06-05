namespace School.API.Core.Entities
{
    public class ExamSubjectSchedule
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int ClassId { get; set; }
        public int ExamId { get; set; }
        public string MinMarks { get; set; }
        public string MaxMarks { get; set; }
        public bool IsAddInTotal { get; set; }
        public bool WillExamConduct { get; set; }
        public DateTime ExamDate { get; set; }
        public int academicYearId { get; set; }
    }
}
