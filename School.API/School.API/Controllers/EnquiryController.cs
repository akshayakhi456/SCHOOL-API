using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.EnquiryRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiry _enquiry;
        public EnquiryController(IEnquiry enquiry) {
        _enquiry = enquiry;
        }

        [HttpGet]
        public IActionResult List() {
            try
            {
                var res = _enquiry.List();
                return StatusCode(200, new APIResponse<List<Enquiry>>((int)HttpStatusCode.OK, "Enqiry List", res));
            }
            catch (Exception ex) {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEnquiryById(int id)
        {
            try
            {
                var res = _enquiry.EnquiryById(id);
                return StatusCode(200, new APIResponse<EnquiryResponseModel>((int)HttpStatusCode.Accepted, "Enqiry List by Id", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(CreateEnquiryRequestModel enquiry) {
            try
            {
                var res = _enquiry.Create(enquiry);
                return StatusCode(200, new APIResponse<Enquiry>((int)HttpStatusCode.OK, "Enqiry Created Successfully", res));
            }
            catch (Exception ex) {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(CreateEnquiryRequestModel enquiry)
        {
            try
            {
                var res = _enquiry.Update(enquiry);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("entranceExamFee")]
        public IActionResult PaymentForEntranceExam(PaymentsEnquiry paymentsEnquiry)
        {
            try
            {
                var res = _enquiry.EntranceExamFee(paymentsEnquiry);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, res));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("changeStatusEnquiryStudent/{id}")]
        public IActionResult ChangeStatusEnquiryStudent(int id,[FromBody] bool status)
        {
            try
            {
                var res = _enquiry.UpdateStatusEnquiryStudent(id,status);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("SaveFeedBack")]
        public IActionResult SaveFeedBack(StudentEnquiryFeedback feedback)
        {
            try
            {
                var res = _enquiry.SaveFeedback(feedback);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("FeedbackList/{id}")]
        public IActionResult FeedBackListById(int id)
        {
            try
            {
                var res = _enquiry.GetFeedback(id);
                return StatusCode(200, new APIResponse<List<StudentEnquiryFeedback>>((int)HttpStatusCode.OK, "Enqiry of Student Feedback List", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

    }
}
