namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class PaymentOfClassWiseStudentsResponseModel
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public long pendingAmount { get; set; }
        public long actualAmount { get; set; }
        public long receivedAmount { get; set; }
    }
}
