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
            
            var classWiseStudentCount = from std in _applicationDbContext.Students
                                        group std by std.className into className
                                        select new
                                        {
                                            className = className.First().className,
                                            count = className.Count()
                                        };
            
            var paymentAllotmentTotalClassWise = _applicationDbContext.paymentAllotments
                            .Where(x => x.acedamicYearId == yearId)
                            .GroupBy(paymentAllotment => paymentAllotment.className).ToList();
            var totalAllotment = paymentAllotmentTotalClassWise.Select(className => new
            {
                ClassName = className.Key,
                Total = className.Sum(x => int.Parse(x.amount)) * classWiseStudentCount.FirstOrDefault(x => x.className == className.Key).count// Parse amount to long
            }).ToList();
            var result = (from record in records
                          group record by record.className into className
                          select new ClassWisePaymentResponseModel
                          {
                              className = className.Key,
                              actualAmount = totalAllotment.FirstOrDefault(x => x.ClassName == className.Key).Total,
                              receivedAmount = className.Sum(c => c.amount),
                              pendingAmount = totalAllotment.FirstOrDefault(x => x.ClassName == className.Key).Total - className.Sum(c => c.amount)
                          }).OrderBy(x => x.className).ToList();
            return result;
        }

        public List<ClassWisePaymentResponseModel> yearWisePayment(int yearId)
        {
            var classWiseReport = classWisePayment(yearId);
            List<ClassWisePaymentResponseModel> finalReport = new List<ClassWisePaymentResponseModel>();
            finalReport.Add(new ClassWisePaymentResponseModel
            {
                actualAmount = 0,
                pendingAmount = 0,
                receivedAmount = 0
            });
            foreach (var item in classWiseReport)
            {
                finalReport[0].actualAmount += item.actualAmount;
                finalReport[0].receivedAmount += item.receivedAmount;
                finalReport[0].pendingAmount += item.pendingAmount;
            }
            return finalReport;
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
            var studentRecords = (from student in _applicationDbContext.Students
                                    where student.className == requestModel.className && (student.section == requestModel.section || student.section == null)
                                    select student).ToList();
            var studentIds = studentRecords.Select(x => x.id).ToList();
            var paymentsRecords = from payment in _applicationDbContext.Payments
                                  where payment.acedamicYearId == requestModel.academicYearId
                                  && studentIds.Contains(payment.studentId)
                                  && requestModel.PaymentAllotmentId.Contains(payment.PaymentAllotmentId)
                                  select payment;
            var sumOfStudentPayment = paymentsRecords.GroupBy(x => x.studentId)
                                    .Select(y => new
                                    {
                                        studentId = y.Key,
                                        receivedAmount = y.Sum(c => c.amount),
                                    });
            var paymentAllotments = (from paymentAllotment in _applicationDbContext.paymentAllotments
                                    where requestModel.PaymentAllotmentId.Contains(paymentAllotment.id)
                                    && paymentAllotment.className == requestModel.className
                                    select paymentAllotment).ToList();
            var totalAmount = paymentAllotments.Sum(c => long.Parse(c.amount));
            var studentPayments = (from student in studentRecords
                                  join sPayment in sumOfStudentPayment on student.id equals sPayment.studentId
                                  into joinedData
                                  from sPayment in joinedData.DefaultIfEmpty()
                                  select new PaymentOfClassWiseStudentsResponseModel
                                  {
                                      studentId = student.id,
                                      studentName = student.firstName + " " + student.lastName,
                                      pendingAmount = sPayment != null ? totalAmount - sPayment.receivedAmount : totalAmount,
                                      actualAmount = totalAmount,
                                      receivedAmount = sPayment != null ? sPayment.receivedAmount : 0
                                  }).OrderBy(x => x.studentName).ToList();
            return studentPayments;
        }
    }
}
