using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.PaymentRequestResponseModel;

namespace School.API.Core.Services
{
    public class PaymentService: IPayment
    {
        private readonly  ApplicationDbContext _applicationDbContext;
        public PaymentService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public bool create(Payments payment)
        {
            _applicationDbContext.Payments.Add(payment);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public List<PaymentResponseModel> list()
        {
            var res = (from payment in _applicationDbContext.Payments
                       join paymentAllotment in _applicationDbContext.paymentAllotments on payment.paymentName equals paymentAllotment.paymentName
                       where payment.paymentName == paymentAllotment.paymentName
                       select new PaymentResponseModel
                       {
                           invoiceId =  payment.invoiceId,
                           paymentName = payment.paymentName,
                           studentId = payment.studentId,
                           amount = payment.amount,
                           dateOfPayment = payment.dateOfPayment,
                           paymentAllotmentAmount = paymentAllotment.amount,
                           remarks = payment.remarks,
                           paymentType = payment.paymentType,
                           paymentAllotmentId = paymentAllotment.id
                       }
                       ).ToList();
            return res;
        }

        public List<PaymentResponseModel> listById(int id)
        {
            var res = (from payment in _applicationDbContext.Payments
                       join paymentAllotment in _applicationDbContext.paymentAllotments on payment.PaymentAllotmentId equals paymentAllotment.id
                       where payment.studentId == id && payment.acedamicYearId == 1
                       select new PaymentResponseModel
                       {
                           invoiceId = payment.invoiceId,
                           paymentName = payment.paymentName,
                           studentId = payment.studentId,
                           amount = payment.amount,
                           dateOfPayment = payment.dateOfPayment,
                           paymentAllotmentAmount = paymentAllotment.amount,
                           remarks = payment.remarks,
                           paymentType = payment.paymentType,
                           paymentAllotmentId = paymentAllotment.id
                       }
                       ).ToList();
            return res;
        }

        public List<ClassWisePaymentResponseModel> classWisePayment(int yearId)
        {
            var records = StudentPaymentRecords(yearId);
            var result = (from record in records
                          group record by record.className into className
                          select new ClassWisePaymentResponseModel
                          {
                              className = className.FirstOrDefault().className,
                              actualAmount = className.Sum(c => long.Parse(c.paymentAllotmentAmount)),
                              receivedAmount = className.Sum(c => c.amount),
                              pendingAmount = className.Sum(c => long.Parse(c.paymentAllotmentAmount)) - className.Sum(c => c.amount)
                          }).ToList();
            return result;
        }

        public List<ClassWisePaymentResponseModel> yearWisePayment(int yearId)
        {
            var records = StudentPaymentRecords(yearId);
            var result = (from record in records
                          group record by record.academicYears into academicYear
                          select new ClassWisePaymentResponseModel
                          {
                              actualAmount = academicYear.Sum(c => long.Parse(c.paymentAllotmentAmount)),
                              receivedAmount = academicYear.Sum(c => c.amount),
                              pendingAmount = academicYear.Sum(c => long.Parse(c.paymentAllotmentAmount)) - academicYear.Sum(c => c.amount)
                          }).ToList();
            return result;
        }

        public List<PaymentResponseModel> StudentPaymentRecords(int yearId)
        {
            return (from payment in _applicationDbContext.Payments
             join paymentAllotment in _applicationDbContext.paymentAllotments on payment.PaymentAllotmentId equals paymentAllotment.id
             where payment.acedamicYearId == yearId
             select new PaymentResponseModel
             {
                 invoiceId = payment.invoiceId,
                 paymentName = payment.paymentName,
                 studentId = payment.studentId,
                 amount = payment.amount,
                 dateOfPayment = payment.dateOfPayment,
                 paymentAllotmentAmount = paymentAllotment.amount,
                 remarks = payment.remarks,
                 paymentType = payment.paymentType,
                 paymentAllotmentId = paymentAllotment.id,
                 className = paymentAllotment.className,
                 academicYears = paymentAllotment.acedamicYearId
             }
             ).ToList();
        }

        public IEnumerable<PaymentOfClassWiseStudentsResponseModel> GetStudentPaymentDataByClassOrSection(PaymentOfClassWiseStudentsRequestModel requestModel)
        {
            var records = StudentPaymentRecordsByAllotment(requestModel);
            var paymentOfStudents = (from record in records
                                     group record by record.studentId into student
                                     select new
                                     {
                                         studentId = student.Key,
                                         actualAmount = student.First().paymentAllotmentAmount,
                                         receivedAmount = student.Sum(c => c.amount),
                                         pendingAmount = long.Parse(student.First().paymentAllotmentAmount) - student.Sum(c => c.amount)
                                     }).ToList();
            var students = (from student in _applicationDbContext.Students
                            where student.className == requestModel.className && (student.section == requestModel.section || 1 == 1) select new { student }).ToList();

            var studentPayments = from student in students 
                                  join sPayment in paymentOfStudents on student.student.id equals sPayment.studentId
                                  into joinedData
                                  from sPayment in joinedData.DefaultIfEmpty()
                                  select new PaymentOfClassWiseStudentsResponseModel
                                  {
                                      studentId = student.student.id,
                                      studentName = student.student.firstName + " " + student.student.lastName,
                                      pendingAmount = sPayment != null ? sPayment.pendingAmount : 0,
                                      actualAmount = sPayment != null ? long.Parse(sPayment.actualAmount) : 0,
                                      receivedAmount = sPayment != null ?  sPayment.receivedAmount : 0
                                  };

            return studentPayments;
        }

        public List<PaymentResponseModel> StudentPaymentRecordsByAllotment(PaymentOfClassWiseStudentsRequestModel requestModel)
        {
            return (from payment in _applicationDbContext.Payments
                    join paymentAllotment in _applicationDbContext.paymentAllotments on payment.PaymentAllotmentId equals paymentAllotment.id
                    where payment.acedamicYearId == requestModel.academicYearId && requestModel.PaymentAllotmentId.Contains(payment.PaymentAllotmentId)
                    select new PaymentResponseModel
                    {
                        invoiceId = payment.invoiceId,
                        paymentName = payment.paymentName,
                        studentId = payment.studentId,
                        amount = payment.amount,
                        dateOfPayment = payment.dateOfPayment,
                        paymentAllotmentAmount = paymentAllotment.amount,
                        remarks = payment.remarks,
                        paymentType = payment.paymentType,
                        paymentAllotmentId = paymentAllotment.id,
                        className = paymentAllotment.className,
                        academicYears = paymentAllotment.acedamicYearId
                    }
             ).ToList();
        }
    }
}
