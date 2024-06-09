using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.API.Core.Entities
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        public string section { get; set; }
        public int? ClassesId { get; set; }
        public virtual Classes Classes { get; set; }
    }
}
