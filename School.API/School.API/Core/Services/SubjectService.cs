using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.StudentMapTeacherRequestResponseModel;
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

        public List<MarksRequestModel> getMarksByClass(int className, int section, int acedemicYearId, int subjectId, int? examId)
        {
            var studentIDs = _applicationDbContext.StudentClassSections.Where(x => x.ClassId == className
            && (x.SectionId == section || section != null)
            && x.AcademicYearId == acedemicYearId)
                .Select(s => s.RollNo).ToList();
            var res = _applicationDbContext.StudentMarks
                .Where(x => studentIDs.Contains(x.RollNo)
                && (x.SubjectId == subjectId || subjectId == 0)
                && (x.ExamId == examId || examId == null)
                && x.AcedamicYearId == acedemicYearId)
                .Select(x => new MarksRequestModel
                {
                    Id=x.Id,
                    AcedamicYearId=x.AcedamicYearId,
                    ExamId=x.ExamId,
                    Marks=x.Marks,
                    Remarks=x.Remarks,
                    RollNo=x.RollNo,
                    Sid=x.Sid,
                    SName=x.SName,
                    SubjectId=x.SubjectId
                })
                .ToList();
            return res;
        }

        public List<ProgressCardResponseModel> progressCardInfo(int? ClassesId, int? SectionId, int? ExamId, int AcademicYearId, int? sid)
        {
            var studentList = _applicationDbContext.StudentClassSections
                .AsNoTracking()
                .Include(x => x.Students)
                .Include(x => x.Section)
                .ThenInclude(x => x.Classes)
                .Where(x => ((x.ClassId == ClassesId
                && (x.SectionId == SectionId || SectionId != null))
                || x.Studentsid == sid)
                && x.AcademicYearId == AcademicYearId)
                .Distinct()
                .ToList();
            var guardians = (from student in studentList
                             join guardian in _applicationDbContext.Guardians on student.Studentsid equals guardian.studentId
                             select new { guardian }).ToList();
            var studentInfo = new List<StudentInfo>();
            foreach (var item in studentList)
            {
                var FatherGuardian = guardians.Where(x => x.guardian.studentId == item.Studentsid && x.guardian.relationship == "Father").ToList();
                var motherGuardian = guardians.Where(x => x.guardian.studentId == item.Studentsid && x.guardian.relationship == "Mother").ToList();

                var std = new StudentInfo();
                std.FatherName = FatherGuardian.Count() > 0 ? $"{FatherGuardian[0].guardian.FirstName} {FatherGuardian[0].guardian.LastName}" : "";
                std.MotherName = motherGuardian.Count() > 0 ? $"{motherGuardian[0].guardian.FirstName} {motherGuardian[0].guardian.LastName}" : "";
                std.DateOfBirth = item.Students.dob;
                std.Id = item.Id;
                std.RollNo = item.RollNo;
                std.Section = item.Section.section;
                std.StudentName = $"{item.Students.firstName} {item.Students.lastName}";
                std.AdmNo = item.Students.id;
                std.ClassName = item.Section.Classes.className;
                studentInfo.Add(std);
            }

            if(sid > 0)
            {
                ClassesId = studentList.FirstOrDefault(x => x.Studentsid == sid).ClassId;
            }

            var progressCardResponse = new List<ProgressCardResponseModel>();
            foreach (var item in studentInfo)
            {
                var cardResponse = new ProgressCardResponseModel();
                var subjectInfo = _applicationDbContext.StudentMarks
                                  .Where(x => x.AcedamicYearId == AcademicYearId && x.ExamId == ExamId && (x.Sid == sid || sid == null))
                                  .SelectMany(x => _applicationDbContext.ExamSubjectSchedules
                                          .Where(ess => ess.SubjectId == x.SubjectId && ess.ClassId == ClassesId && ess.Id == ExamId)
                                          .Select(ess => new SubjectInfo
                                          {
                                              Id = x.Id,
                                              Marks = x.Marks,
                                              Remarks = x.Remarks,
                                              SubjectName = x.Subject.SubjectName,
                                              MaxMarks = ess.MaxMarks,
                                          }))
                                      .ToList();
                cardResponse.StudentInfo = item;
                cardResponse.SubjectInfos = subjectInfo;
                progressCardResponse.Add(cardResponse);
            }
            return progressCardResponse;
        }

        public List<HallTicketResponseModel> hallTicketInfo(int? ClassesId, int? SectionId, int? ExamId, int AcademicYearId, int? sid)
        {
            var studentList = _applicationDbContext.StudentClassSections
                .AsNoTracking()
                .Include(x => x.Students)
                .Include(x => x.Section)
                .ThenInclude(x => x.Classes)
                .Where(x => ((x.ClassId == ClassesId
                && (x.SectionId == SectionId || SectionId != null))
                || x.Studentsid == sid)
                && x.AcademicYearId == AcademicYearId)
                .Distinct()
                .ToList();
            var guardians = (from student in studentList
                             join guardian in _applicationDbContext.Guardians on student.Studentsid equals guardian.studentId
                             select new { guardian }).ToList();
            var studentInfo = new List<StudentInfo>();
            foreach (var item in studentList)
            {
                var FatherGuardian = guardians.Where(x => x.guardian.studentId == item.Studentsid && x.guardian.relationship == "Father").ToList();
                var motherGuardian = guardians.Where(x => x.guardian.studentId == item.Studentsid && x.guardian.relationship == "Mother").ToList();

                var std = new StudentInfo();
                std.FatherName = FatherGuardian.Count() > 0 ? $"{FatherGuardian[0].guardian.FirstName} {FatherGuardian[0].guardian.LastName}" : "";
                std.MotherName = motherGuardian.Count() > 0 ? $"{motherGuardian[0].guardian.FirstName} {motherGuardian[0].guardian.LastName}" : "";
                std.DateOfBirth = item.Students.dob;
                std.Id = item.Id;
                std.RollNo = item.RollNo;
                std.Section = item.Section.section;
                std.StudentName = $"{item.Students.firstName} {item.Students.lastName}";
                std.AdmNo = item.Students.id;
                std.ClassName = item.Section.Classes.className;
                studentInfo.Add(std);
            }

            if (sid > 0)
            {
                ClassesId = studentList.FirstOrDefault(x => x.Studentsid == sid).ClassId;
            }

            var progressCardResponse = new List<HallTicketResponseModel>();
            foreach (var item in studentInfo)
            {
                var cardResponse = new HallTicketResponseModel();
                var subjectInfo = _applicationDbContext.StudentMarks
                                  .Where(x => x.AcedamicYearId == AcademicYearId && x.ExamId == ExamId && (x.Sid == sid || sid == null))
                                  .SelectMany(x => _applicationDbContext.ExamSubjectSchedules
                                          .Where(ess => ess.SubjectId == x.SubjectId && ess.ClassId == ClassesId && ess.Id == ExamId)
                                          .Select(ess => new SubjectDetail
                                          {
                                              ExamDate = ess.ExamDate.ToString("dd-MM-yyyy") +" "+ ess.ExamTime,
                                              SubjectName = x.Subject.SubjectName,
                                          }))
                                      .ToList();
                cardResponse.StudentInfo = item;
                cardResponse.SubjectInfos = subjectInfo;
                progressCardResponse.Add(cardResponse);
            }
            return progressCardResponse;
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
                .Where(x => x.ClassesId == classId
                    && x.academicYearId == academicYearId
                    && (x.SectionId == sectionId || sectionId == null))
                .Select(y => new SubjectResponseModel
                {
                    Id = y.Id,
                    academicYearId = y.academicYearId,
                    ClassesId = y.ClassesId,
                    IsClassTeacher = y.IsClassTeacher,
                    SectionId = y.SectionId,
                    SubjectId = y.SubjectId,
                    TeacherDetailsId = y.TeacherDetailsId,
                    ClassName = y.Classes.className,
                    Section = y.Section.section,
                    Subject = y.Subject.SubjectName,
                    Teacher = $"{y.TeacherDetails.FirstName} {y.TeacherDetails.LastName}"
                }).ToList()
                ;
            return res;
        }

        public string createClassSubjectWithTeacherAssign(List<ClassAssignSubjectTeacher> subjectRequestModel)
        {
            _applicationDbContext.ClassAssignSubjectTeachers.AddRange(subjectRequestModel);
            _applicationDbContext.SaveChanges();
            return "Saved Successfully";
        }

        public string updateClassSubjectWithTeacherAssign(List<ClassAssignSubjectTeacher> classAssignSubjectTeacher)
        {
            foreach (var item in classAssignSubjectTeacher)
            {
                var rec = _applicationDbContext.ClassAssignSubjectTeachers.FirstOrDefault(x => x.Id.Equals(item.Id));
                if (rec is null)
                {
                    throw new EntityInvalidException("Class Subject update", "Record not found.");
                }
                rec.IsClassTeacher = item.IsClassTeacher;
                rec.TeacherDetailsId = item.TeacherDetailsId;
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
