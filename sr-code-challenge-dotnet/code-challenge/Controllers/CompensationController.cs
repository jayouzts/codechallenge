using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {

        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<EmployeeController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}' in the amount of {compensation.Salary.ToString("C")} effecive {compensation.EffectiveDate}");

            _compensationService.Create(compensation);
            

            return CreatedAtRoute("getCurrentCompensationByEmployeeById", new { id = compensation.CompensationId }, compensation);
        }

        [HttpGet("{id}", Name = "getCurrentCompensationByEmployeeById")]
        public IActionResult GetCurrentCompensationByEmployee(String id)
        {
            _logger.LogDebug($"Received employee compensation get request for employee '{id}'");

            var compensation = _compensationService.GetCurrentSalaryByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
