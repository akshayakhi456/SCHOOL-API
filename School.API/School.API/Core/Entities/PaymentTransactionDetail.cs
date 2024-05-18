namespace School.API.Core.Entities
{
    public class PaymentTransactionDetail
    {
        public int id { get; set; }
        public int invoiceId { get; set; }
        public string transactionDetail { get; set; }
    }
}
