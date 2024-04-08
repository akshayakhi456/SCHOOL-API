using School.API.Core.Entities;
using School.API.Core.Models.PaymentRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IPayment
    {
        List<PaymentResponseModel> list();

        List<PaymentResponseModel> listById(int id);
        bool create(Payments payments);

        List<ClassWisePaymentResponseModel> classWisePayment();
        List<ClassWisePaymentResponseModel> yearWisePayment();
    }
}
