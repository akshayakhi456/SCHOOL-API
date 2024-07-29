using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TitleHeaderController : ControllerBase
    {
        private readonly ITitleHeader _titleHeader;
        public TitleHeaderController(ITitleHeader titleHeader)
        {
            _titleHeader = titleHeader;
        }

        [Route("Save")]
        [HttpPost]
        public IActionResult Create([FromForm] IFormCollection fileObj)
        {
            try
            {
                var img = fileObj["file"];
                TitleHeader titleHeader = JsonConvert.DeserializeObject<TitleHeader>(fileObj["titlePayload"]);
                if (!String.IsNullOrEmpty(img))
                {
                    byte[] imgBytes = System.Convert.FromBase64String(img);
                    titleHeader.Photo = imgBytes;
                    var res = _titleHeader.SaveTitle(titleHeader);
                    return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Save Title Header", res));
                }
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "failed"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("list")]
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var res = _titleHeader.getTitleList();
                return StatusCode(200, new APIResponse<List<TitleHeader>>((int)HttpStatusCode.OK, "List Title Header", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [Route("listByQuery")]
        [HttpGet]
        public IActionResult ListByQuery([FromQuery] string query)
        {
            try
            {
                var res = _titleHeader.getTitle(query);
                return StatusCode(200, new APIResponse<TitleHeader>((int)HttpStatusCode.OK, "Title Header Single", res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));
            }
        }
    }
}
