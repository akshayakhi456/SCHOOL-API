using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class EnquiryEntranceExam
    {
        [Key]
        public int id { get; set; }
        [Required]
        public DateTime dateOfExam { get; set; }
        [Required]
        public string modeOfExam { get; set; }
        [Required]
        public string scheduleTimeForExam { get; set; }
        public string enquiryStudentId { get; set; }
    }
}
