using School.API.Core.Entities;
using School.API.Core.Models.SettingRequestResponseModel;
using School.API.Core.Models.SettingsRequestRespnseModel;

namespace School.API.Core.Interfaces
{
    public interface ISettings
    {
        string createClass(Classes classes);
        bool updateClass(Classes classes);
        Classes getClassById(int id);
        List<Classes> getClasses();
        bool deleteClass(int id);
        string createSection(Section section);
        List<SectionResponseModel> getSections();
        List<SectionResponseModel> getSectionsByClassName(int classId);
        bool updateSection(Section section);
        bool deleteSection(int id);
        List<EnquiryQuestions> getAllEnquiryQuestion();
        string createEnquiryQuestion(EnquiryQuestions enquiryQuestions);
        string updateEnquiryQuestion(EnquiryQuestions enquiryQuestions);
        string updateStatusEnquiryQuestion(int id,bool status);
        List<PaymentAllotment> GetPaymentAllotments(int classId);
        string createPaymentAllotment(PaymentAllotment paymentAllotment);
        string updatePaymentAllotment(PaymentAllotment PaymentAllotment);

        List<Subject> subjectList();
        string createSubject(Subject subject);
        string updateSubject(Subject subject);
        string deleteSubject(int id);
        List<Exam> getExams();
        string saveExam(Exam exam);
        string updateExam(Exam exam);
       
    }
}
