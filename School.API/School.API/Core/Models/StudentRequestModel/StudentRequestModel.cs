using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using School.API.Core.Entities;

namespace School.API.Core.Models.StudentRequestModel
{
    public class StudentRequestModel
    {
        public StudentModel students { get; set; }
        public List<Guardian> guardians { get; set; }
        public StudentAddress address { get; set; }


    }
}
