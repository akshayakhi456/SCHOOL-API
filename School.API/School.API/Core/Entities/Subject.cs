using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Subject Name must not be exceed 50 characters")]
        public string SubjectName { get; set; }
    }
}
