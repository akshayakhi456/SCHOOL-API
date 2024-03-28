using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class StudentAddress
    {
        [Key]
        public int id { get; set; }
        public string? HouseNo { get; set; }
        public string? streetName { get; set; }
        public string? city { get; set; }
        public string? district {  get; set; }
        public string? state { get; set; }
        public string? zipCode { get; set; }
        public string? country { get; set; }
        public int? studentId { get; set; }
    }
}
