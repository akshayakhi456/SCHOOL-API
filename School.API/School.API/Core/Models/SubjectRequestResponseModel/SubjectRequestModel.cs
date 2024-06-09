namespace School.API.Core.Models.SubjectRequestResponseModel
{
    public class SubjectRequestModel
    {
        public int Id { get; set; }
        public int ClassesId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherDetailsId { get; set; }
        public bool IsClassTeacher { get; set; }
        public int academicYearId { get; set; }
    }
}
