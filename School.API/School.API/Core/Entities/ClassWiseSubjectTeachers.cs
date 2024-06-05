using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class ClassWiseSubjectTeachers
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int Subject { get; set; }
        public int? TeacherId { get; set; }
        public bool? IsClassTeacher { get; set; }
        public int academicYearId { get; set; }
    }
}
