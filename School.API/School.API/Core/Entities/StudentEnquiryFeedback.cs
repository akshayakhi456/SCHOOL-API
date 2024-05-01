using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class StudentEnquiryFeedback
    {
        [Key]
        public int Id { get; set; }
        public int EnquiryId { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
