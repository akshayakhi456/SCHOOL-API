using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class Students
    {
        [Key]
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
    }
}
