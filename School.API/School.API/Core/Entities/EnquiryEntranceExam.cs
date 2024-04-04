using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class EnquiryEntranceExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
