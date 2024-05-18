using School.API.Core.Entities;
using School.API.Core.Models.PaymentRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IPayment
    {
        List<PaymentResponseModel> list();

        List<PaymentResponseModel> listById(int id);
        bool create(AddPaymentRequest payments);

        List<ClassWisePaymentResponseModel> classWisePayment(int yearId);
        List<ClassWisePaymentResponseModel> yearWisePayment(int yearId);

        IEnumerable<PaymentOfClassWiseStudentsResponseModel> GetStudentPaymentDataByClassOrSection(PaymentOfClassWiseStudentsRequestModel requestModel);
    }
}
