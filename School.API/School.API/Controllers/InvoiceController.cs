using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.API.Common;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.StudentRequestModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoice _invoice;

        public InvoiceController(IInvoice invoice)
        {
            _invoice = invoice;
        }
        [HttpPost]
        public IActionResult create([FromForm] IFormCollection fileObj)
        {
            try
            {
                var img = fileObj["file"];
                InvoiceGenerate invoiceGenerate = JsonConvert.DeserializeObject<InvoiceGenerate>(fileObj["invoice"]);
                if (!String.IsNullOrEmpty(img))
                {
                    byte[] imgBytes = System.Convert.FromBase64String(img);
                    invoiceGenerate.invoicePhoto = imgBytes;
                    var res = _invoice.createInvoice(invoiceGenerate);
                    return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Invoice Saved Successfully."));
                }
                return StatusCode(500, new APIResponse <string>((int)HttpStatusCode.InternalServerError, "failed"));
            }
            catch (EntityInvalidException ex)
            {
                return StatusCode(500, new APIResponse<string>((int) HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("InvoiceId")]
        public IActionResult GetInvoiceId()
        {
            try
            {
                var result = _invoice.lastInvoiceId();
                return StatusCode(200, new APIResponse<string>((int)HttpStatusCode.OK, "Last record Id", result.ToString()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, ex.Message));

            }
        }
    }
}
