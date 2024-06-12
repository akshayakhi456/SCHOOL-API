using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class Guardian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? occupation { get; set; }
        public string? qualification { get; set; }
        public long? contactNumber {  get; set; }
        public string? email { get; set; }
        public string? adharNumber { get; set; }
        public int? studentId { get; set; }

        public string? relationship { get; set; }


    }
}
