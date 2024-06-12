namespace School.API.Core.Models.StudentMapTeacherRequestResponseModel
{
    public class AttendanceRequestModel
    {
        public int? Id { get; set; }
        public int? SId { get; set; }
        public int ClassId { get; set; }
        public int Section { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public int? Date { get; set; }
        public string? AttendanceStatus { get; set; }
    }
}
