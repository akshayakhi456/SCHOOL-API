using School.API.Core.Interfaces;
using School.API.Core.Models.DashoboardRequestResponseModel;
using School.API.Core.Models.PaymentRequestResponseModel;

namespace School.API.Core.Services
{
    public class DashboardService: IDashboard
    {
        private readonly IStudent _studentService;
        private readonly IPayment _paymentService;
        public DashboardService(IStudent studentService, IPayment paymentService) {
            _studentService = studentService;
            _paymentService = paymentService;
        }

        public async Task<DashboardResponse> getInformation(int yearId)
        {
            var studentRecord = await _studentService.list();
            var totalstudentCount = studentRecord.Count;
            DateTime currDate = DateTime.Today;
            var newStudents = studentRecord.Where(x => x.dateOfJoining >= new DateTime(new DateTime().Year, 4, 1)).ToList().Count;
            var todaynewAdmission = studentRecord.Where(x => x.dateOfJoining.Date == currDate.Date).ToList().Count;
            
            DateTime lastWeekStart = currDate.AddDays(-(int)currDate.DayOfWeek).Date;
            DateTime lastWeekEnd = lastWeekStart.AddDays(6).Date;

            var recordsLastWeek = studentRecord
              .Where(entity => entity.dateOfJoining.Date >= lastWeekStart && entity.dateOfJoining.Date <= lastWeekEnd).ToList().Count;

            DateTime firstDayOfLastMonth = currDate.AddMonths(-1).Date;
            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddDays(DateTime.DaysInMonth(firstDayOfLastMonth.Year, firstDayOfLastMonth.Month) - 1).Date;

            var recordsLastMonth = studentRecord
              .Where(entity => entity.dateOfJoining.Date >= firstDayOfLastMonth && entity.dateOfJoining.Date <= lastDayOfLastMonth).ToList().Count;

            var feeCollection = (List<ClassWisePaymentResponseModel>)_paymentService.yearWisePayment(yearId);
            var feePending = feeCollection[0].pendingAmount;
            var feeReceivedAmount = feeCollection[0].receivedAmount;
            return new DashboardResponse()
            {
                feeCollection = (int)feeReceivedAmount,
                feePending = (int)feePending,
                totalStudent = totalstudentCount,
                newAdmissionThisMonth = recordsLastMonth,
                newAdmissionThisWeek = recordsLastWeek,
                newAdmissionToday = todaynewAdmission,
                newStudents = newStudents
            };
        }
    }
}
