using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class Expenses
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string miscellanous { get; set; }
        [Required]
        public string doe { get; set; }
        [Required] 
        public long amount { get; set; }
        public string remarks { get; set; }
    }
}
