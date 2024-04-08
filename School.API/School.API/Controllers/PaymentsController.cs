﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

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
        public IActionResult Create(Payments payments)
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
        [Route("classWiseReport")]
        public IActionResult GetClassWiseReport()
        {
            try
            {
                var res = _payment.classWisePayment();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("yearWiseReport")]
        public IActionResult GetYearWiseReport()
        {
            try
            {
                var res = _payment.yearWisePayment();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
