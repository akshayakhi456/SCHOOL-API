using Microsoft.EntityFrameworkCore;
using School.API.Common;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.SettingRequestResponseModel;
using Section = School.API.Core.Entities.Section;

namespace School.API.Core.Services
{
    public class SettingsService : ISettings
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SettingsService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }
         public string createClass(Classes classes)
        {
            if (classes is not null )
            {
                var isExistClass = _applicationDbContext.classes.Any(c => c.Id == classes.Id);
                if (isExistClass)
                {
                    throw new EntityInvalidException("NotValid", "Already Class Exist");
                }
               _applicationDbContext.classes.Add(classes);
               _applicationDbContext.SaveChanges();
                return "Created Successfully";
            }
            return "Something went wrong";
        }

        public List<Classes> getClasses()
        {
            return _applicationDbContext.classes.ToList();
        }

        public bool updateClass(Classes classes)
        {
            _applicationDbContext.classes.Update(classes);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public Classes getClassById(int id)
        {
            return _applicationDbContext.classes.Find(id);
        }

        public bool deleteClass(int id)
        {
            var rec = _applicationDbContext.classes.Where(x => x.Id == id).FirstOrDefault();
            _applicationDbContext.classes.Remove(rec);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public string createSection(Section section)
        {
            if (section is not null)
            {
                var isExistSection = _applicationDbContext.section.Any(x => x.section == section.section && x.ClassesId == section.ClassesId);
                if (isExistSection) {
                    throw new EntityInvalidException("section","Already Section Exist");
                }
                _applicationDbContext.section.Add(section);
                _applicationDbContext.SaveChanges();
                return "Created Successully";
            }
            throw new EntityInvalidException("Something went wrong");
        }

        public List<SectionResponseModel> getSections()
        {
            return _applicationDbContext.section
                .AsNoTracking()
                .Include(x => x.Classes)
                .Select(x => new SectionResponseModel
                {
                    ClassesId = x.ClassesId ?? 0,
                    id = x.id,
                    section = x.section,
                    ClassName = x.Classes.className
                })
                .ToList();
        }

        public List<SectionResponseModel> getSectionsByClassName(int classId)
        {
            return _applicationDbContext.section
                .AsNoTracking()
                .Include(x => x.Classes)
                .Where(x=>x.ClassesId == classId)
                .Select(x => new SectionResponseModel
                {
                    ClassesId = x.ClassesId ?? 0,
                    id = x.id,
                    section = x.section,
                    ClassName = x.Classes.className
                })
                .ToList();
        }

        public bool updateSection(Section section)
        {
            _applicationDbContext.section.Update(section);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public bool deleteSection(int id)
        {
            var rec = _applicationDbContext.section.Where(x => x.id == id).FirstOrDefault();
            _applicationDbContext.section.Remove(rec);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public string createEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            _applicationDbContext.enquiryQuestions.Add(enquiryQuestions);
            _applicationDbContext.SaveChanges();
            return "Created Successully";
        }

        public string updateEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            _applicationDbContext.enquiryQuestions.Update(enquiryQuestions);
            _applicationDbContext.SaveChanges();
            return "Updated Successfully";
        }

        public List<EnquiryQuestions> getAllEnquiryQuestion()
        {
            var list = _applicationDbContext.enquiryQuestions.ToList();
            return list;
        }

        public string updateStatusEnquiryQuestion(int id, bool status)
        {
            var rec = _applicationDbContext.enquiryQuestions.Where(x => x.id == id).FirstOrDefault();
            rec.status = status;
            _applicationDbContext.SaveChanges();
            return "Updated Successfully";
        }

        public List<PaymentAllotment> GetPaymentAllotments(string className)
        {
            return _applicationDbContext.paymentAllotments.Where(x =>x.className == className).ToList();
        }

        public string createPaymentAllotment(PaymentAllotment paymentAllotment) 
        {
            var isExistPayment = _applicationDbContext.paymentAllotments.Any(x => x.paymentName == paymentAllotment.paymentName && x.className == paymentAllotment.className);
            if (isExistPayment)
            {
                throw new EntityInvalidException("Already Payment Name Exist");
            }
            _applicationDbContext.paymentAllotments.Add(paymentAllotment);
            _applicationDbContext.SaveChanges();
            return "Created Successully";
        }
        public string updatePaymentAllotment(PaymentAllotment paymentAllotment)
        {
            var isExistPayment = _applicationDbContext.paymentAllotments.Any(x => x.paymentName == paymentAllotment.paymentName && x.className == paymentAllotment.className && x.id != paymentAllotment.id);
            if (isExistPayment)
            {
                throw new EntityInvalidException("Already Payment Name Exist");
            }
            _applicationDbContext.paymentAllotments.Update(paymentAllotment);
            _applicationDbContext.SaveChanges();
            return "Updated Successfully";
        }

        public List<Subject> subjectList()
        {
            return _applicationDbContext.Subjects.ToList();
        }

        public string createSubject(Subject subject)
        {
            var isSubjectExist = _applicationDbContext.Subjects.Any(x => x.SubjectName == subject.SubjectName);
            if (isSubjectExist)
            {
                throw new EntityInvalidException("Subject Create", "Subject Already Exist");
            }
            _applicationDbContext.Subjects.Add(subject);
            _applicationDbContext.SaveChanges();
            return "Subject Added Successfully.";
        }

        public string updateSubject(Subject subject)
        {
            var record = _applicationDbContext.Subjects.FirstOrDefault(sub => sub.Id == subject.Id);
            if (record == null)
                throw new EntityInvalidException("Subject Update", "Subject not found");
            record.SubjectName = subject.SubjectName;
            _applicationDbContext.SaveChanges();
            return "Subject Updated Successfully";
        }

        public string deleteSubject(int id)
        {
            var record = _applicationDbContext.Subjects.FirstOrDefault(sub => sub.Id == id);
            if (record == null)
                throw new EntityInvalidException("Subject Delete", "Subject not found");
            _applicationDbContext.Subjects.Remove(record);
            _applicationDbContext.SaveChanges();
            return "Subject Updated Successfully";
        }

        public List<Exam> getExams()
        {
            var res = _applicationDbContext.Exams.ToList();
            return res;
        }

        public string saveExam(Exam exam)
        {
            var examExist = _applicationDbContext.Exams.FirstOrDefault(exam => exam.ExamName.Equals(exam.ExamName));
            if (examExist is Exam)
            {
                Results.Conflict("Exam name already exist.");
            }
            _applicationDbContext.Exams.Add(exam);
            _applicationDbContext.SaveChanges();
            return "Saved Successfully.";
        }

        public string updateExam(Exam exam)
        {
            var examExist = _applicationDbContext.Exams.FirstOrDefault(exam => exam.Id.Equals(exam.Id));
            if (examExist is not Exam)
            {
                Results.Conflict("Exam name not found.");
            }
            examExist.ExamName = exam.ExamName;
            _applicationDbContext.SaveChanges();
            return "Saved Successfully.";
        }
    }
}
