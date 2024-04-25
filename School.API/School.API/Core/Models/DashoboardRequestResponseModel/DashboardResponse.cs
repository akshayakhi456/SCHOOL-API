namespace School.API.Core.Models.DashoboardRequestResponseModel
{
    public class DashboardResponse
    {
        public int totalStudent { get; set; }
        public int newStudents { get; set; }
        public int feeCollection { get; set; }
        public int feePending { get; set; }
        public int newAdmissionToday { get; set; } = 0;
        public int newAdmissionThisWeek { get; set; }
        public int newAdmissionThisMonth { get; set; }
    }
}
