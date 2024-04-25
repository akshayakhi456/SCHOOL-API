using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Common;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ISettings _settings;
        public SettingsController(ISettings settings)
        {
            _settings = settings;
        }
        [HttpGet]
        [Route("classes")]
        public IActionResult GetClass()
        {
            try
            {
                var res = _settings.getClasses();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("classes/{id}")]
        public IActionResult GetClassById(int id)
        {
            try
            {
                var res = _settings.getClassById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("classes")]
        public IActionResult Create(Classes classes)
        {
            try
            {
                var res = _settings.createClass(classes);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (EntityInvalidException ex)
            {
                return StatusCode(400, new APIResponse<string>((int)HttpStatusCode.BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("classes")]
        public IActionResult Update(Classes classes)
        {
            try
            {
                var res = _settings.updateClass(classes);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        [Route("classes/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var res = _settings.deleteClass(id);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            try
            {
                var res = _settings.getSections();
                return CreatedAtAction(nameof(GetClass), new { res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("classes/{className}/sections")]
        public IActionResult GetSectionById(string className)
        {
            try
            {
                var res = _settings.getSectionsByClassName(className);
                return CreatedAtAction(nameof(GetClass), new { res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("section")]
        public IActionResult CreateSection(Section section)
        {
            try
            {
                var res = _settings.createSection(section);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("section")]
        public IActionResult UpdateSection(Section section)
        {
            try
            {
                var res = _settings.updateSection(section);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        [Route("section/{id}")]
        public IActionResult DeleteSection(int id)
        {
            try
            {
                var res = _settings.deleteSection(id);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("enquiryQuestions")]
        public IActionResult GetEnquiryQuestion()
        {
            try
            {
                var res = _settings.getAllEnquiryQuestion();
                return CreatedAtAction(nameof(GetClass), new { result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("enquiryQuestion")]
        public IActionResult CreateEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            try
            {
                var res = _settings.createEnquiryQuestion(enquiryQuestions);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("enquiryQuestion")]
        public IActionResult UpdateEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            try
            {
                var res = _settings.updateEnquiryQuestion(enquiryQuestions);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("ChangeStatusEnquiryQuestion/{id}")]
        public IActionResult ChangeStatusEnquiryQuestion(int id, bool status)
        {
            try
            {
                var res = _settings.updateStatusEnquiryQuestion(id,status);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("paymentAllotment/{className}")]
        public IActionResult GetPaymentAllotment(string className)
        {
            try
            {
                var res = _settings.GetPaymentAllotments(className);
                return CreatedAtAction(nameof(GetClass), new { result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("paymentAllotment")]
        public IActionResult CreatePaymentAllotment(PaymentAllotment paymentAllotment)
        {
            try
            {
                var res = _settings.createPaymentAllotment(paymentAllotment);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("paymentAllotment")]
        public IActionResult UpdatePaymentAllotment(PaymentAllotment paymentAllotment)
        {
            try
            {
                var res = _settings.updatePaymentAllotment(paymentAllotment);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
