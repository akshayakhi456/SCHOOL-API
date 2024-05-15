using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.TeacherRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private ITeacher _teacherService;
        public TeacherController(ITeacher teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(AddTeacherRequest addTeacherRequest)
        {
            try
            {
                var result = _teacherService.create(addTeacherRequest);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Employee Create", result));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Employee Create", ex.Message));
            }
        }
        [HttpPost]
        [Route("update")]
        public IActionResult Update(AddTeacherRequest updateTeacherRequest)
        {
            try
            {
                var result = _teacherService.update(updateTeacherRequest);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Employee Update", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Employee Create", ex.Message));
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _teacherService.getTeacherDetails();
                return StatusCode(200, new APIResponse<List<TeacherDetails>>((int)HttpStatusCode.OK, "Employee Details", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Employee Details", ex.Message));
            }
        }
        [HttpGet]
        [Route("EmployeeApproval/{id}")]
        public IActionResult EmployeeApproval(int id)
        {
            try
            {
                var result = _teacherService.employmentApproval(id);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Employee Approval", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Employee Approval", ex.Message));
            }
        }
        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public IActionResult GetTeacherById(int id)
        {
            try
            {
                var result = _teacherService.getTeacherById(id);
                return StatusCode(200, new APIResponse<AddTeacherRequest>((int)HttpStatusCode.OK, "Employee Detail", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Employee Detail", ex.Message));
            }
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _teacherService.delete(id);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Employee Delete", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Employee Delete", ex.Message));
            }
        }
    }
}
