using School.API.Core.Entities;
using School.API.Core.Models.EnquiryRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IEnquiry
    {
        EnquiryResponseModel EnquiryById(int id);
        List<Enquiry> list();
        Enquiry create(CreateEnquiryRequestModel enquiry);
        string update(CreateEnquiryRequestModel enquiry);
        string entranceExamFee(PaymentsEnquiry paymentsEnquiry);
        string updateStatusEnquiryStudent(int id, bool status);
    }
}
