namespace School.API.Core.Entities
{
    public class StudentLeave
    {
        public int Id { get; set; }
        public string PurposeOfLeave { get; set; }
        public int NoOfDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remarks { get; set; }
        public bool Approval { get; set; } = false;
        public int StudentId { get; set; }
        public int? AcademicYearId  { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }

    }
}
