using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.ExamRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExam _exam;
        private readonly IMapper _mapper;
        public ExamController(IExam exam, IMapper mapper) {
            _exam = exam;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("addExamsDetails")]
        public IActionResult AddExamDetails(List<ExamRequestModel> examRequestModel)
        {
            try
            {
                var examSubjectSchedules =_mapper.Map<List<ExamSubjectSchedule>>(examRequestModel);
                var result = _exam.addExamSubjectSchedule(examSubjectSchedules);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Exam Subject add", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Exam Subject add", ex.Message));
            }
        }

        [HttpPut]
        [Route("updateExamsDetails")]
        public IActionResult UpdateExamDetails(List<ExamRequestModel> examRequestModel)
        {
            try
            {
                var examSubjectSchedules = _mapper.Map<List<ExamSubjectSchedule>>(examRequestModel);
                var result = _exam.upateExamSubjectSchedule(examSubjectSchedules);
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Exam Subject update", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Exam Subject update", ex.Message));
            }
        }

        [HttpGet]
        [Route("getExamsDetails")]
        public IActionResult GetExamDetails([FromQuery]int academicYearId, int classId, int examId)
        {
            try
            {
                var result = _exam.examSubjectSchedules(academicYearId, classId, examId);
                return StatusCode(200, new APIResponse<List<ExamResponseModel>>((int)HttpStatusCode.OK, "Exam Subject list", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Exam Subject list", ex.Message));
            }
        }
    }
}
