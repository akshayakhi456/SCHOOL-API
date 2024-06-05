using School.API.Core.Entities;

namespace School.API.Core.Interfaces
{
    public interface IExam
    {
        List<ExamSubjectSchedule> examSubjectSchedules(int academicYearId, int classId, int examId);
        string addExamSubjectSchedule(List<ExamSubjectSchedule> examSubjectSchedules);
        string upateExamSubjectSchedule(List<ExamSubjectSchedule> examSubjectSchedules);
    }
}
