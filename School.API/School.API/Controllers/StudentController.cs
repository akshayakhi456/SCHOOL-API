using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Middleware;
using School.API.Core.Models.StudentRequestModel;
using School.API.Core.Models.Wrappers;
using System.Net;
using System.Security.Claims;


namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class StudentController : Controller
    {
        private readonly IStudent _studentService;
        public StudentController(IStudent studentService) {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _studentService.list();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var res = await _studentService.StudentById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create([FromForm] IFormCollection fileObj)
        {
            try
            {
                var img = fileObj["file"];
                StudentGuardianRequest oStudentGuardian = JsonConvert.DeserializeObject<StudentGuardianRequest>(fileObj["studentGuardian"]);
                if (!String.IsNullOrEmpty(img))
                {
                    byte[] imgBytes = System.Convert.FromBase64String(img);
                    oStudentGuardian.students.photo = imgBytes;
                    var res = await _studentService.create(oStudentGuardian);
                    return Ok(res);
                }
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "failed"));
            }
            catch (Exception ex) {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> update([FromForm] IFormCollection fileObj)
        {
            try
            {
                var img = fileObj["file"];
                StudentGuardianRequest oStudentGuardian = JsonConvert.DeserializeObject<StudentGuardianRequest>(fileObj["studentGuardian"]);
                if (!String.IsNullOrEmpty(img))
                {
                    byte[] imgBytes = System.Convert.FromBase64String(img);
                    oStudentGuardian.students.photo = imgBytes;
                    var res = await _studentService.update(oStudentGuardian);
                    return Ok(res);
                }
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "failed"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("getStudentByClassName/{className}")]
        public IActionResult GetStudentByClassId([FromRoute]string className)
        {
            try
            {
                var res = _studentService.StudentsByClassName(className);
                return StatusCode(200, new APIResponse<List<StudentGuardianRequest>>((int)HttpStatusCode.OK, "Students By ClassName",res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("getStudentsByRoles")]
        public IActionResult GetStudentByRole()
        {
            try
            {
                var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var res = _studentService.GetStudentsByRole(role, email);
                return StatusCode(200, new APIResponse<List<StudentGuardianRequest>>((int)HttpStatusCode.OK, "Students By Role", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
