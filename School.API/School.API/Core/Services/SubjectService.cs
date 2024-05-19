using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Core.Services
{
    public class SubjectService : ISubject
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SubjectService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public string addMarks(List<StudentMarks> studentMarks)
        {
            foreach (var mark in studentMarks)
            {
                if(mark.Id == 0)
                {
                    _applicationDbContext.StudentMarks.Add(mark);
                    _applicationDbContext.SaveChanges();
                }
                else
                {
                    var student = _applicationDbContext.StudentMarks.FirstOrDefault(x => x.Id == mark.Id);
                    if (student != null)
                    {
                        student.Marks = mark.Marks;
                        _applicationDbContext.SaveChanges();
                    }
                }
            }
            return "Marks Saved Successfully";
        }

        public List<StudentMarks> getMarksByStudentId(int studentId, int acedemicYearId)
        {
            var res = _applicationDbContext.StudentMarks.Where(x => x.Sid == studentId && x.AcedamicYearId == acedemicYearId).ToList();
            return res;
        }

        public List<StudentMarks> getMarksByClass(string className, string section, int acedemicYearId)
        {
            var studentIDs = _applicationDbContext.StudentClassSections.Where(x => x.ClassName ==  className 
            && (x.Section == section || String.IsNullOrEmpty(section)) 
            && x.AcademicYear == acedemicYearId)
                .Select(s => s.RollNo).ToList();
            var res = _applicationDbContext.StudentMarks.Where(x => studentIDs.Contains(x.rollNo) && x.AcedamicYearId == acedemicYearId).ToList();
            return res;
        }
    }
}
