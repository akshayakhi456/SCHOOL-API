namespace School.API.Core.Entities
{
    public class ClassAssignSubjectTeacher
    {
        public int Id { get; set; }
        public int ClassesId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherDetailsId { get; set; }
        public bool IsClassTeacher { get; set; }
        public int academicYearId { get; set; }
        public virtual Classes Classes { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Section Section { get; set; }
        public virtual TeacherDetails TeacherDetails { get; set; }
    }
}
