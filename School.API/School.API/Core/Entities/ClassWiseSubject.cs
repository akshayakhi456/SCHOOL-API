namespace School.API.Core.Entities
{
    public class ClassWiseSubjects
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int academicYearId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
