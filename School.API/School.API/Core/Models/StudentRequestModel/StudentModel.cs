using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Models.StudentRequestModel
{
    public class StudentModel
    {
        public int id { get; set; }

        [Required]
        public string firstName { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public int? classesId { get; set; }
        public string section { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public string gender { get; set; }
        public byte[] photo { get; set; }
        public long adharNumber { get; set; }
        public string? sibilings { get; set; }
        public string? certificateNames { get; set; }
        public DateTime dateOfJoining { get; set; } = new DateTime();
        public int? CurrentClassName { get; set; }

    }
}
