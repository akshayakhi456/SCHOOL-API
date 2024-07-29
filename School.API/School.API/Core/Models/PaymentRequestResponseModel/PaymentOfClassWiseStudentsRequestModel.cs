namespace School.API.Core.Models.PaymentRequestResponseModel
{
    public class PaymentOfClassWiseStudentsRequestModel
    {
        public int classId { get; set; }
        public string? section { get; set; }
        public int academicYearId { get; set; }
        public List<int> PaymentAllotmentId { get; set; }
    }
}
