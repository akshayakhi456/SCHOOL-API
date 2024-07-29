using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.PaymentRequestResponseModel;
using School.API.Migrations;

namespace School.API.Core.Services
{
    public class PaymentService: IPayment
    {
        private readonly  ApplicationDbContext _applicationDbContext;
        public PaymentService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public bool create(AddPaymentRequest payment)
        {
            _applicationDbContext.Payments.Add(payment.Payments);
            _applicationDbContext.SaveChanges();
            if (payment.Payments.paymentType != "Cash")
            {
                payment.PaymentTransactionDetails.invoiceId = payment.Payments.invoiceId;
                _applicationDbContext.PaymentTransactionDetails.Add(payment.PaymentTransactionDetails);
                _applicationDbContext.SaveChanges(true);
            }
            return true;
        }

        public List<PaymentResponseModel> list()
        {
            var res = (from payment in _applicationDbContext.Payments
                       join paymentAllotment in _applicationDbContext.paymentAllotments on payment.paymentName equals paymentAllotment.paymentName
                       //join paymentDetail in _applicationDbContext.PaymentTransactionDetails on payment.invoiceId equals paymentDetail.invoiceId
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
                           paymentAllotmentId = paymentAllotment.id,
                           //transactionDetail = paymentDetail
                       }
                       ).ToList();
            return res;
        }

        public List<PaymentResponseModel> listById(int id)
        {
            var res = (from payment in _applicationDbContext.Payments
                       join paymentAllotment in _applicationDbContext.paymentAllotments on payment.PaymentAllotmentId equals paymentAllotment.id
                       join paymentDetail in _applicationDbContext.PaymentTransactionDetails on payment.invoiceId equals paymentDetail.invoiceId into transaction
                       from paymentDetails in  transaction.DefaultIfEmpty()
                       where payment.studentId == id
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
                           transactionDetail = paymentDetails,
                           academicYears = payment.acedamicYearId
                       }
                       ).ToList();
            return res;
        }

        public List<ClassWisePaymentResponseModel> classWisePayment(int yearId)
        {
            var records = StudentPaymentRecords(yearId);
            
            var classWiseStudentCount = from std in _applicationDbContext.Students
                                        group std by std.classesId into className
                                        select new
                                        {
                                            className = className.First().classes.className,
                                            classId = className.First().classes.Id,
                                            count = className.Count()
                                        };
            
            var paymentAllotmentTotalClassWise = _applicationDbContext.paymentAllotments
                            .Where(x => x.acedamicYearId == yearId && classWiseStudentCount.Any(y => y.classId == x.classId))
                            .GroupBy(paymentAllotment => paymentAllotment.classId).ToList();
            var totalAllotment = paymentAllotmentTotalClassWise.Select(className => new
            {
                ClassName = classWiseStudentCount.First(x => x.classId == className.Key).className,
                ClassId = className.Key,
                Total = className.Sum(x => int.Parse(x.amount)) * classWiseStudentCount.FirstOrDefault(x => x.classId == className.Key).count// Parse amount to long
            }).OrderBy(x => x.ClassName).ToList();
            var classWiseSum = (from record in records
                                group record by record.classId into className
                                select new
                                {
                                    classId = className.Key,
                                    receivedAmount = className.Sum(c => c.amount),
                                });

            var result = new List<ClassWisePaymentResponseModel>();
            foreach (var record in totalAllotment)
            {
                ClassWisePaymentResponseModel model = new ClassWisePaymentResponseModel();
                model.classId = record.ClassId;
                model.className = record.ClassName;
                
                var classWiseSumItem = classWiseSum.FirstOrDefault(x => x.classId == record.ClassId);
                if (classWiseSumItem != null)
                {
                    model.receivedAmount = classWiseSumItem.receivedAmount;
                    model.pendingAmount = record.Total - classWiseSumItem.receivedAmount;
                }
                else
                {
                    // Handle the case where no matching item is found in classWiseSum
                    model.receivedAmount = 0; // or any default value you prefer
                    model.pendingAmount = record.Total; // assuming pending amount is total amount if no record found
                }
                model.actualAmount = record.Total;
                result.Add(model);
            }
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
                 classId = paymentAllotment.classId,
                 academicYears = paymentAllotment.acedamicYearId
             }
             ).ToList();
        }

        public IEnumerable<PaymentOfClassWiseStudentsResponseModel> GetStudentPaymentDataByClassOrSection(PaymentOfClassWiseStudentsRequestModel requestModel)
        {
            var studentRecords = (from student in _applicationDbContext.Students
                                    where student.classesId == requestModel.classId && (student.section == requestModel.section || requestModel.section == null)
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
                                    && paymentAllotment.classId == requestModel.classId
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
