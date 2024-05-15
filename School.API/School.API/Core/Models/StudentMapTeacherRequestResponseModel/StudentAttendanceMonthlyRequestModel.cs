namespace School.API.Core.Models.StudentMapTeacherRequestResponseModel
{
    public class StudentAttendanceMonthlyRequestModel
    {
        public string? SId { get; set; }
        public string? ClassName { get; set; }
        public string? Section { get; set; }
        public int StartMonth { get; set; }
        public string StartYear { get; set; }
        public int? EndMonth { get; set; }
        public string? EndYear { get; set; }
    }
}
