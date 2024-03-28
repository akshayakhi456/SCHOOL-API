using Microsoft.AspNetCore.Http.HttpResults;
using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.EnquiryRequestResponseModel;

namespace School.API.Core.Services
{
    public class EnquiryService : IEnquiry
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EnquiryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<bool> create(CreateEnquiryRequestModel createEnquiryRequestModel)
        {
           _applicationDbContext.Enquiry.Add(createEnquiryRequestModel.enquiry);
            _applicationDbContext.SaveChanges();
            if (createEnquiryRequestModel.enquiry.id != 0)
            {
                createEnquiryRequestModel.enquiryEntranceExam.enquiryStudentId = createEnquiryRequestModel.enquiry.id.ToString();
                _applicationDbContext.EnquiryEntranceExams.Add(createEnquiryRequestModel.enquiryEntranceExam);
                _applicationDbContext.SaveChanges();
            }
            return Task.FromResult(true);
        }

        public Task<EnquiryResponseModel> EnquiryById(int id)
        {
            var enquiryDetails = _applicationDbContext.Enquiry.Where(x => x.id == id).SingleOrDefault();
            var entranceDetails = _applicationDbContext.EnquiryEntranceExams.Where(x => x.enquiryStudentId == id.ToString()).SingleOrDefault();
            var paymentDetails = _applicationDbContext.paymentsEnquiry.Where(x => x.studentEnquireId == id).FirstOrDefault();
            return Task.FromResult(new EnquiryResponseModel()
            {
                enquiry = enquiryDetails,
                enquiryEntranceExam = entranceDetails,
                paymentsEnquiry = paymentDetails
            });
        }

        public Task<List<Enquiry>> list()
        {
            var res = _applicationDbContext.Enquiry.ToList();
            return Task.FromResult(res);
        }

        public Task<bool> update(CreateEnquiryRequestModel enquiry)
        {
            var res = _applicationDbContext.Enquiry.Where(x=>x.id == enquiry.enquiry.id).SingleOrDefault();
            res.firstName = enquiry.enquiry.firstName;
            res.lastName = enquiry.enquiry.lastName;
            res.address = enquiry.enquiry.address;
            res.previousSchoolName = enquiry.enquiry.previousSchoolName;
            res.status = enquiry.enquiry.status;
            res.mobile = enquiry.enquiry.mobile;
            res.dob = enquiry.enquiry.dob;
            res.className = enquiry.enquiry.className;
            res.guardian = enquiry.enquiry.guardian;
            _applicationDbContext.SaveChanges();
            var examDetails = _applicationDbContext.EnquiryEntranceExams.Where(x => x.enquiryStudentId == enquiry.enquiryEntranceExam.enquiryStudentId).SingleOrDefault();
            examDetails.dateOfExam = enquiry.enquiryEntranceExam.dateOfExam;
            examDetails.modeOfExam = enquiry.enquiryEntranceExam.modeOfExam;
            examDetails.scheduleTimeForExam = enquiry.enquiryEntranceExam.scheduleTimeForExam;
            _applicationDbContext.SaveChanges();
            return Task.FromResult(true);
        }
        
        public Task<bool> entranceExamFee(PaymentsEnquiry paymentsEnquiry)
        {
            var isExist = _applicationDbContext.paymentsEnquiry.Any(x => x.studentEnquireId == paymentsEnquiry.studentEnquireId);
            if (isExist)
            {
                var res = _applicationDbContext.paymentsEnquiry.Where(x => x.studentEnquireId == paymentsEnquiry.studentEnquireId).FirstOrDefault();
                res.amount = paymentsEnquiry.amount;
                res.dateOfPayment = paymentsEnquiry.dateOfPayment;
                res.paymentStatus = paymentsEnquiry.paymentStatus;
                res.paymentType = paymentsEnquiry.paymentType;
            }
            else
            {
            _applicationDbContext.paymentsEnquiry.Add(paymentsEnquiry);
            }
            _applicationDbContext.SaveChanges(); 
            return Task.FromResult(true);
        }
    }
}
