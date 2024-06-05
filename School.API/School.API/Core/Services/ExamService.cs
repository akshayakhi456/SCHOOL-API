using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Core.Services
{
    public class ExamService : IExam
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ExamService(ApplicationDbContext  applicationDbContext) {
        _applicationDbContext = applicationDbContext;
        }
        public string addExamSubjectSchedule(List<ExamSubjectSchedule> examSubjectSchedules)
        {
            _applicationDbContext.ExamSubjectSchedules.AddRange(examSubjectSchedules);
            _applicationDbContext.SaveChanges();
            return "Saved Successfully";
        }

        public string upateExamSubjectSchedule(List<ExamSubjectSchedule> examSubjectSchedules)
        {
            foreach (var item in examSubjectSchedules)
            {
                var rec = _applicationDbContext.ExamSubjectSchedules.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (rec is not ExamSubjectSchedule)
                {
                    throw new EntityInvalidException("Update Exam Subjects", "Record not found");
                }
                rec.MinMarks = item.MinMarks;
                rec.MaxMarks = item.MaxMarks;
                rec.WillExamConduct = item.WillExamConduct;
                rec.IsAddInTotal = item.IsAddInTotal;
                _applicationDbContext.SaveChanges();
            }
            return "Saved Successfully";
        }

        public List<ExamSubjectSchedule> examSubjectSchedules(int academicYearId, int classId, int examId)
        {
            var record = _applicationDbContext.ExamSubjectSchedules.Where(exam =>
            exam.academicYearId == academicYearId &&
            exam.ClassId == classId &&
            exam.ExamId == examId
            ).ToList();
            return record;
        }
    }
}
