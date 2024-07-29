using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Common;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.SettingRequestResponseModel;
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
        private readonly IMapper _mapper;
        public SettingsController(ISettings settings, IMapper mapper)
        {
            _settings = settings;
            _mapper = mapper;
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
        [Route("classes/{classId}/sections")]
        public IActionResult GetSectionById(int classId)
        {
            try
            {
                var res = _settings.getSectionsByClassName(classId);
                return CreatedAtAction(nameof(GetClass), new { res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("section")]
        public IActionResult CreateSection(SectionRequestModel sectionRequestModel)
        {
            try
            {
                var model = _mapper.Map<Section>(sectionRequestModel);
                var res = _settings.createSection(model);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("section")]
        public IActionResult UpdateSection(SectionRequestModel sectionRequestModel)
        {
            try
            {
                var model = _mapper.Map<Section>(sectionRequestModel);
                var res = _settings.updateSection(model);
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
                var res = _settings.updateStatusEnquiryQuestion(id, status);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("paymentAllotment/{className}")]
        public IActionResult GetPaymentAllotment(int className)
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

        [HttpGet]
        [Route("subjectList")]
        public IActionResult SubjectList()
        {
            try
            {
                var res = _settings.subjectList();
                return StatusCode(200, new APIResponse<List<Subject>>((int)HttpStatusCode.OK, "List Subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("subjectCreate")]
        public IActionResult SubjectCreate(Subject subject)
        {
            try
            {
                var res = _settings.createSubject(subject);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Create Subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("subjectUpdate")]
        public IActionResult SubjectUpdate(Subject subject)
        {
            try
            {
                var res = _settings.updateSubject(subject);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Update Subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        [Route("subjectDelete/{id}")]
        public IActionResult SubjectDeletet(int id)
        {
            try
            {
                var res = _settings.deleteSubject(id);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Delete Subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("exams")]
        public IActionResult GetExamNames()
        {
            try
            {
                var res = _settings.getExams();
                return StatusCode(200, new APIResponse<List<Exam>>((int)HttpStatusCode.OK, "Get Exams", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("exams")]
        public IActionResult SaveExamName(Exam exam)
        {
            try
            {
                var res = _settings.saveExam(exam);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Save Exams", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("exams")]
        public IActionResult UpdateExamName(Exam exam)
        {
            try
            {
                var res = _settings.updateExam(exam);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Update Exams", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

       
    }
}
