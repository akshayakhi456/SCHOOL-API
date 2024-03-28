using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class PaymentsEnquiry
    {
        [Key]
        public int invoiceId { get; set; }
        [Required]
        public string feeName { get; set; }
        [Required]
        public string paymentType { get; set; }
        [Required]
        public long amount { get; set; }
        [Required]
        public string remarks { get; set; }
        [Required]
        public DateTime dateOfPayment { get; set; }
        [Required]
        public int studentEnquireId { get; set; }
        public string paymentStatus { get; set; } = "Pending";
    }
}
