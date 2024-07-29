using School.API.Core.Entities;
using School.API.Core.Models.StudentRequestModel;

namespace School.API.Core.Interfaces
{
    public interface IStudent
    {
        Task<StudentGuardianRequest> StudentById(int id);
        Task<List<StudentGuardianRequest>> StudentBykey(string key);
        Task<List<Students>> list();
        Task<bool> create(StudentGuardianRequest student);
        Task<bool> update(StudentGuardianRequest student);
        List<StudentGuardianRequest> GetStudentsByRole(string role, string email);
        List<StudentGuardianRequest> StudentsByClassName(int className);
        string ApplyLeave(StudentLeave studentLeave);
        string ApproveLeave(int id);
        List<StudentLeave> GetStudentLeave(int academicYearId, int sid);
        List<studentApprovalResponse> GetStudentLeaveForTeacher(int academicYearId, int classId, int? section);
        Task<string> StudentBulkUpload(List<StudentRequestModel> StudentRequestModel);
    }
}
