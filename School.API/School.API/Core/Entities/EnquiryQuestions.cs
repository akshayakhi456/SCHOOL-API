using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class EnquiryQuestions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [MaxLength(255,ErrorMessage = "Question must not be exceed 255 characters")]
        public string question { get; set; }
        public string type { get; set; }
        public string options { get; set; }
        public bool isRequired { get; set; }
        public bool isMultiple { get; set; }
        public bool status { get; set; }
    }
}
