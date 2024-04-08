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

        public List<ClassWisePaymentResponseModel> classWisePayment()
        {
            var records = StudentPaymentRecords();
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

        public List<ClassWisePaymentResponseModel> yearWisePayment()
        {
            var records = StudentPaymentRecords();
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

        public List<PaymentResponseModel> StudentPaymentRecords()
        {
            return (from payment in _applicationDbContext.Payments
             join paymentAllotment in _applicationDbContext.paymentAllotments on payment.PaymentAllotmentId equals paymentAllotment.id
             where payment.acedamicYearId == 1
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
