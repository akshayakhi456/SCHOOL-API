using School.API.Core.Entities;
using School.API.Core.Models.SubjectRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface ISubject
    {
        string addMarks(List<StudentMarks> studentMarks);

        List<StudentMarks> getMarksByStudentId(int studentId, int acedemicYearId);

        List<StudentMarks> getMarksByClass(string className, string section, int acedemicYearId, string subject);
        string deleteClassWiseSubject(int id);
        string addClassWiseSubjects(ClassWiseSubjects classWiseSubjects);
        List<SubjectResponseModel> classSubjectWithTeacherAssign();
        string createClassSubjectWithTeacherAssign(SubjectRequestModel subjectRequestModel);
        string updateClassSubjectWithTeacherAssign(ClassAssignSubjectTeacher classWiseSubjectTeachers);
        string deleteClassSubjectWithTeacherAssign(int id);
        IReadOnlyList<ClassWiseSubjectResponse> getClassSubject(int academicYearId, int classId);
    }
}
