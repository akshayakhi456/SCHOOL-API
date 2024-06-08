using Microsoft.EntityFrameworkCore;
using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.ExamRequestResponseModel;

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

       public List<ExamResponseModel> examSubjectSchedules(int academicYearId, int classId, int examId)
{
    var records = _applicationDbContext.ExamSubjectSchedules
        .AsNoTracking()
        .Include(x => x.Subject)
        .Where(exam =>
            exam.AcademicYearId == academicYearId &&
            exam.ClassId == classId &&
            exam.ExamId == examId
        )
        .Select(x => new ExamResponseModel
        {
            AcademicYearId = x.AcademicYearId,
            ExamId = x.ExamId,
            WillExamConduct = x.WillExamConduct,
            ClassId = x.ClassId,
            ExamDate = x.ExamDate,
            Id = x.Id,
            IsAddInTotal = x.IsAddInTotal,
            MaxMarks = x.MaxMarks,
            MinMarks = x.MinMarks,
            SubjectId = x.SubjectId,
            SubjectName = x.Subject.SubjectName
        })
        .ToList();

    return records;
}
    }
}
