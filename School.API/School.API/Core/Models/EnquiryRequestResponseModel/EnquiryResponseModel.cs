using School.API.Core.Entities;

namespace School.API.Core.Models.EnquiryRequestResponseModel
{
    public class EnquiryResponseModel
    {
        public Enquiry enquiry { get; set; }
        public EnquiryEntranceExam enquiryEntranceExam { get; set; }
        public PaymentsEnquiry paymentsEnquiry { get; set; }
    }
}
