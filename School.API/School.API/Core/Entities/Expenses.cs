using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class Expenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string miscellanous { get; set; }
        [Required]
        public DateTime doe { get; set; }
        [Required] 
        public long amount { get; set; }
        public string remarks { get; set; }
    }
}
