using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.StudentMapTeacherRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMapTeacherController : ControllerBase
    {
        private IStudentMapTeacher _studentMapTeacher;
        public StudentMapTeacherController(IStudentMapTeacher studentMapTeacher)
        {
            _studentMapTeacher = studentMapTeacher;
        }

        [HttpPost]
        [Route("StudentAttendance")]
        public IActionResult StudentAttendance(List<StudentAttendance> studentAttendance)
        {
            try
            {
                var result = _studentMapTeacher.studentAttendance(studentAttendance);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Student Attendance", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Student Attendance", ex.Message));
            }
        }

        [HttpPost]
        [Route("UpdateStudentAttendance")]
        public IActionResult updateStudentAttendance(List<AttendanceRequestModel> studentAttendance)
        {
            try
            {
                var result = _studentMapTeacher.updateStudentAttendance(studentAttendance);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Student Attendance", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Student Attendance", ex.Message));
            }
        }

        [HttpPost]
        [Route("GetStudentAttendance")]
        public IActionResult GetStudentAttendance(AttendanceRequestModel attendanceRequestModel)
        {
            try
            {
                var result = _studentMapTeacher.getStudentAttendance(attendanceRequestModel);
                return StatusCode(200, new APIResponse<List<StudentAttendance>>((int)HttpStatusCode.OK, "Student Attendance", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Student Attendance", ex.Message));
            }
        }

        [HttpPost]
        [Route("GetStudentAttendanceByMonthYear")]
        public IActionResult getStudentAttendanceByMonthYear(StudentAttendanceMonthlyRequestModel attendanceRequestModel)
        {
            try
            {
                var result = _studentMapTeacher.GetStudentAttendanceByMonthYear(attendanceRequestModel);
                return StatusCode(200, new APIResponse<List<StudentAttendance>>((int)HttpStatusCode.OK, "Student Attendance", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Student Attendance", ex.Message));
            }
        }

        [HttpPost]
        [Route("StudentAssignSection")]
        public IActionResult studentAssignSection(List<StudentClassSection> studentClassSections)
        {
            try
            {
                var result = _studentMapTeacher.studentAssignSection(studentClassSections);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Student Assign Section", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Student Assign Section", ex.Message));
            }
        }

        [HttpGet]
        [Route("StudentAssignSection")]
        public IActionResult getListOfStudent([FromQuery]string ClassName, string? section, int academicYearId)
        {
            try
            {
                var result = _studentMapTeacher.getListOfStudent(ClassName, section, academicYearId);
                return StatusCode(200, new APIResponse<List<StudentClassSection>>((int)HttpStatusCode.OK, "Student Assign Section", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Student Assign Section", ex.Message));
            }
        }
    }
}
