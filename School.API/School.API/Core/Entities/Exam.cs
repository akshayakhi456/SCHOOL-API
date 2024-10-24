using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "Exam must not be exceed 50 characters")]
        public string ExamName { get; set; }
    }
}
