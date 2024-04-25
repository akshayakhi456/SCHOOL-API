using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int invoiceId { get; set; }
        [Required]
        public string paymentName { get; set; }
        [Required]
        public string paymentType { get; set;}
        [Required]
        public long amount { get; set;}
        [Required]
        public string remarks { get; set; }
        [Required]
        public DateTime dateOfPayment { get; set; }
        [Required]
        [ForeignKey("Students")]
        public int studentId { get; set; }
        [Required]
        [ForeignKey("PaymentAllotment")]
        public int PaymentAllotmentId { get; set; }
        [Required]
        public int acedamicYearId { get; set; }
        public DateTime? dueDateOfPayment { get; set; }
    }
}
