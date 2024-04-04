using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class Students
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
        public string className { get; set; }
        public string section { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public string gender { get; set; }
        public byte[] photo { get; set; }
        public long adharNumber { get; set; }
        public string?  sibilings { get; set; }
        public string? certificateNames { get;set; }
    }
}
