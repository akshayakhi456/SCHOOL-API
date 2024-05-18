using School.API.Core.Entities;

namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class AddPaymentRequest
    {
        public Payments Payments { get; set; }
        public PaymentTransactionDetail PaymentTransactionDetails { get; set; }
    }
}
