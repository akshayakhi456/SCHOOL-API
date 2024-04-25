using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfConverterController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Policy = IdentityData.UserRolePolicyName)]
        public  async Task<IActionResult> GeneratePdf()
        {
            var document = new PdfDocument();
            //string htmlContent = "<div style='display: flex; gap: 20px;'>";
            string htmlContent = "<div>";
            htmlContent += "<div>Parent Copy</div>";
            htmlContent += "<div>";
            htmlContent += "<h1 style='margin-top: 0px;margin-bottom: 0px;text-align: center;'>Receipt</h1>";
            htmlContent += "</div>";
            htmlContent += "<div style='float: right;'>Receipt No: 1</div>";
            htmlContent += "<div>";
            htmlContent += "<div style='text-align: center; text-align: center; padding: 20px 0px; border: 1px solid;'>";
            htmlContent += "<div>";
            htmlContent += "<h2 style='margin-top: 0px;margin-bottom: 0px;'>SchoolUI Demo School</h2>";
            htmlContent += "</div>";
            htmlContent += "<div>";
            htmlContent += "<h4 style='margin-top: 0px;margin-bottom: 0px;'>Charminar Hyderabad</h4>";
            htmlContent += "</div>";
            htmlContent += "</div>";
            htmlContent += "<div>";
            htmlContent += "<table style='width: 100%'>";
            htmlContent += "<tr>";
            htmlContent += "<td>Student Name: std";
            htmlContent += "<td style='float: right'>class: 1";
            htmlContent += "</tr>";
            htmlContent += "</table>";
            htmlContent += "</div>";
            //htmlContent += "<div>";
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string FileName = "Invoice_1.pdf";
            return File(response, "application/pdf", FileName);
        }

        [NonAction]
        public string InvoiceHtml()
        {
            string htmlContent = "<div style=\"display: flex; gap: 20px;\">";
            htmlContent += "<div>";
            htmlContent += "<div>Parent Copy</div>";
            htmlContent += "<div>";
            htmlContent += "<h1 style='margin-top:0px;margin-bottom: 0px;text-align: center;' > Receipt </ h1 >";
            htmlContent += "</div>";
            htmlContent += "<div style='text-align: end;'> Receipt No: 1 </div>";
            htmlContent += "<div>";
            htmlContent += "<div style='text-align: center; text-align: center; padding: 20px 0px; border: 1px solid;'>";
            htmlContent += "<div>";
            htmlContent += "<h2 style='margin-top: 0px;margin-bottom: 0px;'> SchoolUI Demo School</h2>";
            htmlContent += "</div>";
            htmlContent += "<div>";
            htmlContent += "<h4 style='margin-top: 0px;margin-bottom: 0px;'> Charminar Hyderabad </h4>";
            htmlContent += "</div>";
            htmlContent += "</div>";
            htmlContent += "<div style='display: flex;justify-content: space-between;margin-top: 10px;margin-bottom: 10px;'>";
            htmlContent += "<div>";
            htmlContent += "<div> Student : StudentName </div>";
            htmlContent += "< div > FatherName : Father Name</div>";
            htmlContent += "</div>";
            htmlContent += "<div>";
            htmlContent += "<div> Class : Grade 1 </div>";
            htmlContent += "<div> Student Reg No: SHF01 </div>";
            htmlContent += "</div>";
            htmlContent += "</div>";
            htmlContent += "<div>";
            htmlContent += "<table style='width: 100%; border: 1px solid black; border-collapse: collapse;'>";
            htmlContent += "<thead>";
            htmlContent += "<tr>";
            htmlContent += "<th style='border: 1px solid black; border-collapse: collapse;'> S.No </th>";
            htmlContent += "<th style='border: 1px solid black; border-collapse: collapse;'> Particulars </th>";
            htmlContent += "<th style='border: 1px solid black; border-collapse: collapse;'> Amount </th>";
            htmlContent += "</tr>";
            htmlContent += "</thead>";
            htmlContent += "<tbody style='text-align: center;'>";
            htmlContent += "<tr>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'>1</td>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'>Tution Fee</td>";
            htmlContent += "<td style ='border: 1px solid black; border-collapse: collapse;'>Rs. 2000 </td>";
            htmlContent += "</tr>";
            htmlContent += "<tr>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'> 2 </td>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'> Admission Fee </td>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'> Rs. 2500 </td>";
            htmlContent += "</tr>";
            htmlContent += "<tr>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'> 3 </td>";
            htmlContent += "< td style='border: 1px solid black; border-collapse: collapse;'> Term Fee </td>";
            htmlContent += "<td style='border: 1px solid black; border-collapse: collapse;'> Rs. 500 </td>";
            htmlContent += "</tr>";
            htmlContent += "</tbody>";
            htmlContent += "</table>";
            htmlContent += "</div>";
            htmlContent += "< div style='display: flex; justify-content: space-between;margin-top: 40px;'>";
            htmlContent += "<div> Parent's Signature</div>";
            htmlContent += "<div> Received By </div>";
            htmlContent += "</div>";
            htmlContent += "< div style='display: flex; justify-content: space-between;' >";
            htmlContent += "<small> Date: 2 / 4 / 2024 </small>";
            htmlContent += "<div> Cashier's Signature</div>";
            htmlContent += "</div>";
            htmlContent += "</div>";
            htmlContent += "</div>";
            return htmlContent;
        }
    }
}
