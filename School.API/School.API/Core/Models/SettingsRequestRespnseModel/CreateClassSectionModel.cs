using School.API.Core.Entities;

namespace School.API.Core.Models.SettingsRequestRespnseModel
{
    public class CreateClassSectionModel
    {
        public Classes classes {  get; set; }
        public List<Section> section { get; set; }
    }
}
