using School.API.Core.Entities;
using School.API.Core.Models.EnquiryRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IEnquiry
    {
        EnquiryResponseModel EnquiryById(int id);
        List<Enquiry> List();
        Enquiry Create(CreateEnquiryRequestModel enquiry);
        string Update(CreateEnquiryRequestModel enquiry);
        string EntranceExamFee(PaymentsEnquiry paymentsEnquiry);
        string UpdateStatusEnquiryStudent(int id, bool status);

        List<StudentEnquiryFeedback> GetFeedback(int id);

        string SaveFeedback(StudentEnquiryFeedback feedback);
    }
}
