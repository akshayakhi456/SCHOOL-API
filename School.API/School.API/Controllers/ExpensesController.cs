using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenses _expenses;
        public ExpensesController(IExpenses expenses) {
            _expenses = expenses;
        }

        [HttpGet]
        public IActionResult list()
        {
            try
            {
                var res = _expenses.list();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex);
            }
        }

        [HttpPost]
        [Route("create")]
        public  IActionResult create(Expenses expenses)
        {
            try
            {
                var res = _expenses.create(expenses);
                return Ok(res);
            } catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
