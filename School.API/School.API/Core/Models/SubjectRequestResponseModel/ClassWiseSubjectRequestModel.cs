namespace School.API.Core.Models.SubjectRequestResponseModel
{
    public class ClassWiseSubjectRequestModel
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int academicYearId { get; set; }
    }
}
