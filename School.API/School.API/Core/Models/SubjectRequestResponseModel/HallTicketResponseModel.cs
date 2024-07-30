namespace School.API.Core.Models.SubjectRequestResponseModel
{
    public class HallTicketResponseModel
    {
        public StudentInfo StudentInfo { get; set; }
        public List<SubjectDetail> SubjectInfos { get; set; }
    }

    public class SubjectDetail
    {
        public string SubjectName { get; set; }
        public string ExamDate { get; set; }
    }
}
