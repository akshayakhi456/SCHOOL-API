using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using School.API.Core.Identity;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfConverterController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = IdentityData.AdminRolePolicyName)]
        public  async Task<IActionResult> GeneratePdf(string InvoiceId)
        {
            var document = new PdfDocument();
            string HtmlContent = "<h1>Welcome to Skool UI </h1>";
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string FileName = "Invoice_1.pdf";
            return File(response, "application/pdf", FileName);
        }
    }
}
