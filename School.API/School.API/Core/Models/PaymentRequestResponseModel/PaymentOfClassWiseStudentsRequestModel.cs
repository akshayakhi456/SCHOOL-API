namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class PaymentOfClassWiseStudentsRequestModel
    {
        public string className { get; set; }
        public string section { get; set; }
        public int academicYearId { get; set; }
        public List<int> PaymentAllotmentId { get; set; }
    }
}
