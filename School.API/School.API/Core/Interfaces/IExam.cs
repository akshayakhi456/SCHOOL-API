using School.API.Core.Entities;
using School.API.Core.Models.ExamRequestResponseModel;

namespace School.API.Core.Interfaces
{
    public interface IExam
    {
        List<ExamResponseModel> examSubjectSchedules(int academicYearId, int classId, int examId);
        string addExamSubjectSchedule(List<ExamSubjectSchedule> examSubjectSchedules);
        string upateExamSubjectSchedule(List<ExamSubjectSchedule> examSubjectSchedules);
    }
}
