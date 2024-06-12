namespace School.API.Core.Models.StudentMapTeacherRequestResponseModel
{
    public class StudentAttendanceMonthlyRequestModel
    {
        public int? SId { get; set; }
        public int? ClassName { get; set; }
        public int? Section { get; set; }
        public int StartMonth { get; set; }
        public string StartYear { get; set; }
        public int? EndMonth { get; set; }
        public string? EndYear { get; set; }
    }
}
