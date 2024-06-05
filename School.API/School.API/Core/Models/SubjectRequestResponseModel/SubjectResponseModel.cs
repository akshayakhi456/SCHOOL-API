namespace School.API.Core.Models.SubjectRequestResponseModel
{
    public class SubjectResponseModel: SubjectRequestModel
    {
        public string ClassName { get; set; }
        public string Subject { get; set; }
        public string Section { get; set; }
        public string Teacher { get; set; }
    }
}
