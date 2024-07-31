using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;
using School.API.Core.Models.PaymentRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPayment _payment;
        public PaymentsController(IPayment payment)
        {
            _payment = payment;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var res = _payment.list();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetListById(int id)
        {
            try
            {
                var res = _payment.listById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(AddPaymentRequest payments)
        {
            try
            {
                var res = _payment.create(payments);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("classWiseReport/{yearId}")]
        public IActionResult GetClassWiseReport(int yearId)
        {
            try
            {
                var res = _payment.classWisePayment(yearId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("yearWiseReport/{yearId}")]
        public IActionResult GetYearWiseReport(int yearId)
        {
            try
            {
                var res = _payment.yearWisePayment(yearId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("studentsRecordOfPayment")]
        public IActionResult GetMonthWiseReport(PaymentOfClassWiseStudentsRequestModel requestModel)
        {
            try
            {
                var result = _payment.GetStudentPaymentDataByClassOrSection(requestModel);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, new APIResponse<string>((int)HttpStatusCode.InternalServerError, "Something went wrong", ex.Message));
            }
        }
    }
}
