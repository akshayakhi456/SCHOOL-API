using School.API.Core.Entities;
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
        List<Section> getSections();
        List<Section> getSectionsByClassName(string className);
        bool updateSection(Section section);
        bool deleteSection(int id);
        List<EnquiryQuestions> getAllEnquiryQuestion();
        string createEnquiryQuestion(EnquiryQuestions enquiryQuestions);
        string updateEnquiryQuestion(EnquiryQuestions enquiryQuestions);
        string updateStatusEnquiryQuestion(int id,bool status);
        List<PaymentAllotment> GetPaymentAllotments(string className);
        string createPaymentAllotment(PaymentAllotment paymentAllotment);
        string updatePaymentAllotment(PaymentAllotment PaymentAllotment);

        List<Subject> subjectList();
        string createSubject(Subject subject);
        string updateSubject(Subject subject);
        string deleteSubject(int id);
    }
}
