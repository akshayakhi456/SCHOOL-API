using School.API.Core.Entities;

namespace School.API.Core.Models.EnquiryRequestResponseModel
{
    public class CreateEnquiryRequestModel
    {
        public Enquiry enquiry { get; set; }
        public EnquiryEntranceExam enquiryEntranceExam { get; set; }
    }
}
