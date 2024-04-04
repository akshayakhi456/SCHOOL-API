using School.API.Core.Entities;
using School.API.Core.Models.EnquiryRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IEnquiry
    {
        Task<EnquiryResponseModel> EnquiryById(int id);
        Task<List<Enquiry>> list();
        Task<Enquiry> create(CreateEnquiryRequestModel enquiry);
        Task<bool> update(CreateEnquiryRequestModel enquiry);
        Task<bool> entranceExamFee(PaymentsEnquiry paymentsEnquiry);
    }
}
