using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class PaymentAllotment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string paymentName { get; set; }
        public string amount { get; set; }
        public string className { get; set; }

    }
}
