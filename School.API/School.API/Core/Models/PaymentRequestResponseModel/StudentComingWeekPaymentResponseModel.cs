namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class StudentComingWeekPaymentResponseModel
    {
        public string StudentName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
    }
}
