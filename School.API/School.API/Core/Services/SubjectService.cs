using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.SubjectRequestResponseModel;

namespace School.API.Core.Services
{
    public class SubjectService : ISubject
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        public SubjectService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }
        public string addMarks(List<StudentMarks> studentMarks)
        {
            foreach (var mark in studentMarks)
            {
                if (mark.Id == 0)
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

        public List<StudentMarks> getMarksByClass(string className, string section, int acedemicYearId, int subjectId, int examId)
        {
            var studentIDs = _applicationDbContext.StudentClassSections.Where(x => x.ClassName == className
            && (x.Section == section || String.IsNullOrEmpty(section))
            && x.AcademicYear == acedemicYearId)
                .Select(s => s.RollNo).ToList();
            var res = _applicationDbContext.StudentMarks.Where(x => studentIDs.Contains(x.RollNo)
            && x.SubjectId == subjectId
            && x.ExamId == examId
            && x.AcedamicYearId == acedemicYearId).ToList();
            return res;
        }

        public IReadOnlyList<ClassWiseSubjectResponse> getClassSubject(int academicYearId, int classId)
        {
            return _applicationDbContext.ClassWiseSubjects
                    .AsNoTracking()
                    .Include(x => x.Subject)
                    .Where(x => x.academicYearId.Equals(academicYearId)
                    && x.ClassId.Equals(classId)).Select(x => new ClassWiseSubjectResponse
                    {
                        Id = x.Id,
                        Subject = x.Subject.SubjectName
                    }).ToList();
        }

        public string addClassWiseSubjects(ClassWiseSubjects classWiseSubjects)
        {
            var record = _applicationDbContext.ClassWiseSubjects.FirstOrDefault(x =>
            x.ClassId.Equals(classWiseSubjects.ClassId)
            && x.academicYearId.Equals(classWiseSubjects.academicYearId)
            && x.SubjectId.Equals(classWiseSubjects.SubjectId));
            if (record is ClassWiseSubjects)
            {
                throw new EntityInvalidException("Add Class Wise Subject", "Record already exist");
            }
            _applicationDbContext.ClassWiseSubjects.Add(classWiseSubjects);
            _applicationDbContext.SaveChanges();
            return "Add Class Subject";
        }

        public string deleteClassWiseSubject(int id)
        {
            var recordExist = _applicationDbContext.ClassWiseSubjects.FirstOrDefault(se =>
                 se.Id == id);
            if (recordExist is not ClassWiseSubjects)
            {
                throw new EntityInvalidException("Class Wise subject delete", "record may not be found");
            }
            var res = _applicationDbContext.ClassWiseSubjects.Remove(recordExist);
            _applicationDbContext.SaveChanges();
            return "Removed Successfully";
        }

        public List<SubjectResponseModel> classSubjectWithTeacherAssign(int classId, int academicYearId, int? sectionId)
        {
            var res = _applicationDbContext.ClassAssignSubjectTeachers
                .AsNoTracking()
                .Include(x => x.Classes)
                .Include(x => x.Subject)
                .Include(x => x.Section)
                .Where(x => x.ClassId == classId
                    && x.academicYearId == academicYearId
                    && (x.SectionId == sectionId || sectionId == null))
                .Select(y => new SubjectResponseModel
                {
                    Id = y.Id,
                    academicYearId = y.academicYearId,
                    ClassId = y.ClassId,
                    IsClassTeacher = y.IsClassTeacher,
                    SectionId = y.SectionId,
                    SubjectId = y.SubjectId,
                    SubjectTeacherId = y.SubjectTeacherId,
                    ClassName = y.Classes.className,
                    Section = y.Section.section,
                    Subject = y.Subject.SubjectName,
                    Teacher = $"{y.TeacherDetails.FirstName} {y.TeacherDetails.LastName}"
                }).ToList()
                ;
            return res;
        }

        public string createClassSubjectWithTeacherAssign(List<SubjectRequestModel> subjectRequestModel)
        {
            var model = _mapper.Map<ClassAssignSubjectTeacher>(subjectRequestModel);
            _applicationDbContext.ClassAssignSubjectTeachers.AddRange(model);
            _applicationDbContext.SaveChanges();
            return "Saved Successfully";
        }

        public string updateClassSubjectWithTeacherAssign(List<SubjectRequestModel> classAssignSubjectTeacher)
        {
            foreach (var item in classAssignSubjectTeacher)
            {
                var rec = _applicationDbContext.ClassAssignSubjectTeachers.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (rec is null)
                {
                    throw new EntityInvalidException("Class Subject update", "Record not found.");
                }
                rec.IsClassTeacher = item.IsClassTeacher;
                rec.SubjectTeacherId = item.SubjectTeacherId;
                _applicationDbContext.SaveChanges();
            }
            return "Saved Successfully";
        }

        public string deleteClassSubjectWithTeacherAssign(int id)
        {
            var recordExist = _applicationDbContext.ClassAssignSubjectTeachers.FirstOrDefault(se =>
                se.Id == id);
            if (recordExist is not ClassAssignSubjectTeacher)
            {
                throw new EntityInvalidException("Class Wise delete", "record may not be found");
            }
            var res = _applicationDbContext.ClassAssignSubjectTeachers.Remove(recordExist);
            _applicationDbContext.SaveChanges();
            return "Removed Successfully";
        }
    }
}
