using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class Enquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string firstName { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public long mobile { get; set; }
        [Required]
        public string guardian { get; set; }
        [Required]
        public string className { get; set; }
        [Required]
        public string reference { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public string previousSchoolName { get; set; }
        public string? parentInteraction { get; set; }
        public int? rating { get; set; }
        public string? review { get; set; }
    }
}
