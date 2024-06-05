namespace School.API.Core.Models.SubjectRequestResponseModel
{
    public class SubjectRequestModel
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectTeacherId { get; set; }
        public bool IsClassTeacher { get; set; }
        public int academicYearId { get; set; }
    }
}
