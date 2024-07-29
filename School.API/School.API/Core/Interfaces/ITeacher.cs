using School.API.Core.Entities;
using School.API.Core.Models.TeacherRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface ITeacher
    {
        Task<string> create(AddTeacherRequest addTeacherRequest);
        string update(AddTeacherRequest updateTeacherRequest);
        string delete(int id);
        string employmentApproval(int id);
        List<TeacherDetails> getTeacherDetails();
        AddTeacherRequest getTeacherById(int id);
    }
}
