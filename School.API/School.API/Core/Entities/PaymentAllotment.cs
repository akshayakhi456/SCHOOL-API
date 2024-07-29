using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class PaymentAllotment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string paymentName { get; set; }
        [Required]
        public string amount { get; set; }
        [Required]
        public int classId { get; set; }
        [Required]
        public int acedamicYearId { get; set; }

    }
}
