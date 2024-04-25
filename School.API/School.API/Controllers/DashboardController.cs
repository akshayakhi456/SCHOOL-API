using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.API.Core.Interfaces;
using School.API.Core.Models.DashoboardRequestResponseModel;
using School.API.Core.Models.Wrappers;
using System.Net;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboard _dashboardService;

        public DashboardController(IDashboard dashboardService)
        {
                _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("{yearId}")]
        public IActionResult GetInformation(int yearId)
        {
            var result = _dashboardService.getInformation(yearId);
            return StatusCode(200, new APIResponse<Task<DashboardResponse>>((int)HttpStatusCode.OK, "Dashboard Data", result));
        }
    }
}
