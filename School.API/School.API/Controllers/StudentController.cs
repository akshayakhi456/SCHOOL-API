using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using School.API.Core.Interfaces;
using School.API.Core.Models.StudentRequestModel;


namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var res = await _studentService.StudentById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                    //using (var ms = new MemoryStream())
                    //{
                        //img.CopyTo(ms);
                        //var fileBytes = ms.ToArray();
                        //oStudentGuardian.students.photo = fileBytes;
                        var res = await _studentService.create(oStudentGuardian);
                        //return Ok(res);
                    //}
                }
                return BadRequest("failed");
            }
            catch (Exception ex) {
                return StatusCode(500,ex);
            }
        }
    }
}
