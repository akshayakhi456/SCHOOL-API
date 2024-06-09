using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Models.SettingRequestResponseModel
{
    public class SectionRequestModel
    {
        public int id { get; set; }
        public string section { get; set; }
        public int ClassesId { get; set; }
    }
}
