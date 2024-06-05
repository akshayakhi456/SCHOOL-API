using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.SubjectRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubject _subject;
        private readonly IMapper _mapper;
        public SubjectController(ISubject subject, IMapper mapper)
        {
            _subject = subject;
            _mapper = mapper;
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
        public IActionResult GetMarksByID([FromQuery] string className, string section, int acedemicYearId, string subject)
        {
            try
            {
                var res = _subject.getMarksByClass(className, section, acedemicYearId,subject);
                return StatusCode(200, new APIResponse<List<StudentMarks>>((int)HttpStatusCode.OK, "Subject Marks list", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Subject Marks list", ex.Message));
            }
        }

        [HttpGet]
        [Route("SubjectTeacher")]
        public IActionResult GetSubjectTeachers()
        {
            try
            {
                var res = _subject.classSubjectWithTeacherAssign();
                return StatusCode(200, new APIResponse<List<SubjectResponseModel>>((int)HttpStatusCode.OK, "Get Subject Exams", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("SubjectTeacher")]
        public IActionResult SaveSubjectTeacher(SubjectRequestModel subjectRequestModel)
        {
            try
            {
                var res = _subject.createClassSubjectWithTeacherAssign(subjectRequestModel);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Save subject Exams", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        [Route("SubjectTeacher")]
        public IActionResult DeleteSubjectTeacher(int id)
        {
            try
            {
                var res = _subject.deleteClassSubjectWithTeacherAssign(id);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Delete Subject Exams", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("classSubject")]
        public IActionResult GetSubjectExam([FromQuery]int academicYearId,int classId)
        {
            try
            {
                var res = _subject.getClassSubject(academicYearId, classId);
                return StatusCode(200, new APIResponse<IReadOnlyList<ClassWiseSubjectResponse>>((int)HttpStatusCode.OK, "Get Class wise subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("classSubject")]
        public IActionResult SaveSubjectExam(ClassWiseSubjectRequestModel classWiseSubjects)
        {
            try
            {
                var model = _mapper.Map<ClassWiseSubjects>(classWiseSubjects);
                var res = _subject.addClassWiseSubjects(model);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Save Class wise subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpDelete]
        [Route("classSubject")]
        public IActionResult DeleteExamName(int id)
        {
            try
            {
                var res = _subject.deleteClassWiseSubject(id);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Delete Class Wise Subject", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
