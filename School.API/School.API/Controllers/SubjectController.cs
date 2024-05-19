using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.PaymentRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubject _subject;
        public SubjectController(ISubject subject)
        {
            _subject = subject;
        }
        [HttpPost]
        [Route("addMarks")]
        public IActionResult Create(List<StudentMarks> studentMarks)
        {
            try
            {
                var res = _subject.addMarks(studentMarks);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Subject Marks Save", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Subject Marks Save", ex.Message));
            }
        }

        [HttpGet]
        [Route("getMarks")]
        public IActionResult GetMarks([FromQuery] int studentId, int acedemicYearId)
        {
            try
            {
                var res = _subject.getMarksByStudentId(studentId, acedemicYearId);
                return StatusCode(200, new APIResponse<List<StudentMarks>>((int)HttpStatusCode.OK, "Subject Marks list", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Subject Marks list", ex.Message));
            }
        }

        [HttpGet]
        [Route("getMarksByClass")]
        public IActionResult GetMarksByID([FromQuery] string className, string section, int acedemicYearId)
        {
            try
            {
                var res = _subject.getMarksByClass(className, section, acedemicYearId);
                return StatusCode(200, new APIResponse<List<StudentMarks>>((int)HttpStatusCode.OK, "Subject Marks list", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Subject Marks list", ex.Message));
            }
        }
    }
}
