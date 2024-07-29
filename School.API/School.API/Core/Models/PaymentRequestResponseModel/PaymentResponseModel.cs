using School.API.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class PaymentResponseModel
    {
        public int invoiceId { get; set; }
        public string paymentName { get; set; }
        public string paymentType { get; set; }
        public long amount { get; set; }
        public string remarks { get; set; }
        public DateTime dateOfPayment { get; set; }
        public int studentId { get; set; }
        public int paymentAllotmentId { get; set; }
        public string paymentAllotmentAmount { get; set; }
        public int classId { get; set; }
        public int academicYears { get; set; }
        public PaymentTransactionDetail? transactionDetail { get; set; }
    }
}
