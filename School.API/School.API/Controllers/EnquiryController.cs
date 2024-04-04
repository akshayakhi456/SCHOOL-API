using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.EnquiryRequestResponseModel;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                var res = _enquiry.list();
                return Ok(res);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEnquiryById(int id)
        {
            try
            {
                var res = _enquiry.EnquiryById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(CreateEnquiryRequestModel enquiry) {
            try
            {
                var res = _enquiry.create(enquiry);
                return Ok(res);
            } catch (Exception ex) {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(CreateEnquiryRequestModel enquiry)
        {
            try
            {
                var res = _enquiry.update(enquiry);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("entranceExamFee")]
        public IActionResult PaymentForEntranceExam(PaymentsEnquiry paymentsEnquiry)
        {
            try
            {
                var res = _enquiry.entranceExamFee(paymentsEnquiry);
                return Ok(res);
            }catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("changeStatusEnquiryStudent/{id}")]
        public IActionResult ChangeStatusEnquiryStudent(int id, bool status)
        {
            try
            {
                var res = _enquiry.updateStatusEnquiryStudent(id,status);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
