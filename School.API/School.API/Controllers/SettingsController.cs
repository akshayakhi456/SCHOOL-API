﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettings _settings;
        public SettingsController(ISettings settings)
        {
            _settings = settings;
        }
        [HttpGet]
        [Route("classes")]
        public IActionResult GetClass()
        {
            try
            {
                var res = _settings.getClasses();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("classes/{id}")]
        public IActionResult GetClassById(int id)
        {
            try
            {
                var res = _settings.getClassById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("classes")]
        public IActionResult Create(Classes classes)
        {
            try
            {
                var res = _settings.createClass(classes);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("classes")]
        public IActionResult Update(Classes classes)
        {
            try
            {
                var res = _settings.updateClass(classes);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        [Route("classes/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var res = _settings.deleteClass(id);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            try
            {
                var res = _settings.getSections();
                return CreatedAtAction(nameof(GetClass), new { res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("classes/{className}/sections")]
        public IActionResult GetSectionById(string className)
        {
            try
            {
                var res = _settings.getSectionsByClassName(className);
                return CreatedAtAction(nameof(GetClass), new { res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("section")]
        public IActionResult CreateSection(Section section)
        {
            try
            {
                var res = _settings.createSection(section);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("section")]
        public IActionResult UpdateSection(Section section)
        {
            try
            {
                var res = _settings.updateSection(section);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete]
        [Route("section/{id}")]
        public IActionResult DeleteSection(int id)
        {
            try
            {
                var res = _settings.deleteSection(id);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("enquiryQuestions")]
        public IActionResult GetEnquiryQuestion()
        {
            try
            {
                var res = _settings.getAllEnquiryQuestion();
                return CreatedAtAction(nameof(GetClass), new { result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("enquiryQuestion")]
        public IActionResult CreateEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            try
            {
                var res = _settings.createEnquiryQuestion(enquiryQuestions);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("enquiryQuestion")]
        public IActionResult UpdateEnquiryQuestion(EnquiryQuestions enquiryQuestions)
        {
            try
            {
                var res = _settings.updateEnquiryQuestion(enquiryQuestions);
                return CreatedAtAction(nameof(GetClass), new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}