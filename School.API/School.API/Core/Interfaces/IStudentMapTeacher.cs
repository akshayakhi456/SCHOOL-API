using School.API.Core.Entities;
using School.API.Core.Models.StudentMapTeacherRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IStudentMapTeacher
    {
        string studentAttendance(List<StudentAttendance> studentAttendance);
        string updateStudentAttendance(List<AttendanceRequestModel> attendanceRequestModel);
        List<StudentAttendance> getStudentAttendance(AttendanceRequestModel attendanceRequestModel);
        List<StudentAttendance> GetStudentAttendanceByMonthYear(StudentAttendanceMonthlyRequestModel attendanceRequestModel);
        string studentAssignSection(List<StudentClassSection> studentClassSections);
        List<StudentMapClassResponseModel> getListOfStudent(int ClassId, int? section, int academicYearId);

    }
}
