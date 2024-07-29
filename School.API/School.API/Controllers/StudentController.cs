using AutoMapper;
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
using static System.Collections.Specialized.BitVector32;


namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorize]
    public class StudentController : Controller
    {
        private readonly IStudent _studentService;
        private IMapper _mapper;
        public StudentController(IStudent studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
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
        public async Task<IActionResult> GetById([FromRoute] int id)
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

        [HttpGet]
        [Route("studentByKey")]
        public async Task<IActionResult> GetByKey([FromQuery] string q)
        {
            try
            {
                var res = await _studentService.StudentBykey(q);
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
                var result = JsonConvert.DeserializeObject<StudentRequestModel>(fileObj["studentGuardian"]);
                StudentGuardianRequest oStudentGuardian = new StudentGuardianRequest();
                oStudentGuardian.students = _mapper.Map<Students>(result.students);
                oStudentGuardian.address = result.address;
                oStudentGuardian.guardians = result.guardians;
                if (!String.IsNullOrEmpty(img))
                {
                    byte[] imgBytes = System.Convert.FromBase64String(img);
                    oStudentGuardian.students.photo = imgBytes;
                    var res = await _studentService.create(oStudentGuardian);
                    return Ok(res);
                }
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "failed"));
            }
            catch (Exception ex)
            {
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
        [Route("getStudentByClassName/{classId}")]
        public IActionResult GetStudentByClassId([FromRoute] int classId)
        {
            try
            {
                var res = _studentService.StudentsByClassName(classId);
                return StatusCode(200, new APIResponse<List<StudentGuardianRequest>>((int)HttpStatusCode.OK, "Students By ClassName", res));
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

        [HttpPost]
        [Route("LeaveApproval/{id}")]
        public IActionResult StudentLeaveApply([FromRoute] int id)
        {
            try
            {
                var res = _studentService.ApproveLeave(id);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Student Leave Apply", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("ApplyLeave")]
        public IActionResult StudentLeaveApprove(StudentLeave studentLeave)
        {
            try
            {
                var res = _studentService.ApplyLeave(studentLeave);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Student Leave Apply", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("GetStudentLeave")]
        public IActionResult GetStudentLeave([FromQuery] int academicYearId, int id)
        {
            try
            {
                var res = _studentService.GetStudentLeave(academicYearId, id);
                return StatusCode(200, new APIResponse<List<StudentLeave>>((int)HttpStatusCode.OK, "Get Student Leave Apply", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("GetStudentLeaveForTeacher")]
        public IActionResult GetStudentLeaveForTeacher([FromQuery] int academicYearId, int classId, int sectionId)
        {
            try
            {
                var res = _studentService.GetStudentLeaveForTeacher(academicYearId, classId, sectionId);
                return StatusCode(200, new APIResponse<List<studentApprovalResponse>>((int)HttpStatusCode.OK, "Get Student Leave Apply For Teacher", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("StudentBulkUpload")]
        public IActionResult BulkUpload(List<StudentRequestModel> StudentRequestModel)
        {
            try
            {
                var res = _studentService.StudentBulkUpload(StudentRequestModel);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Student Bulk Upload", res.Result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
