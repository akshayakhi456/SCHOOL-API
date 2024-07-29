using School.API.Core.Entities;

namespace School.API.Core.Models.StudentRequestModel
{
    public class studentApprovalResponse: StudentLeave
    {
        public int RollNo { get; set; }
        public string StudentName { get; set; }
    }
}
