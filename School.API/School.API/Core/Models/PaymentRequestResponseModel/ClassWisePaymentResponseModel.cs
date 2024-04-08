namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class ClassWisePaymentResponseModel
    {
        public string? className { get; set; }
        public long actualAmount { get; set; }
        public long receivedAmount { get; set; }
        public long pendingAmount { get; set; }
    }
}
