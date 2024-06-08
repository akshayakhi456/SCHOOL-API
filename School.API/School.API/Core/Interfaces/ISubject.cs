using School.API.Core.Entities;
using School.API.Core.Models.SubjectRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface ISubject
    {
        string addMarks(List<StudentMarks> studentMarks);

        List<StudentMarks> getMarksByStudentId(int studentId, int acedemicYearId);

        List<StudentMarks> getMarksByClass(string className, string section, int acedemicYearId, int subjectId, int examId);
        string deleteClassWiseSubject(int id);
        string addClassWiseSubjects(ClassWiseSubjects classWiseSubjects);
        List<SubjectResponseModel> classSubjectWithTeacherAssign(int classId, int academicYearId, int? sectionId);
        string createClassSubjectWithTeacherAssign(List<SubjectRequestModel> subjectRequestModel);
        string updateClassSubjectWithTeacherAssign(List<SubjectRequestModel> classWiseSubjectTeachers);
        string deleteClassSubjectWithTeacherAssign(int id);
        IReadOnlyList<ClassWiseSubjectResponse> getClassSubject(int academicYearId, int classId);
    }
}
