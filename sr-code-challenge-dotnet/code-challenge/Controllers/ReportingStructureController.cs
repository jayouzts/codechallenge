﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/reportingstructure")]
  
    public class ReportingStructureController : Controller
    {

        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getReportingStructureByEmployeeId")]
        public IActionResult GetReportingStructureByEmployeeId(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            var reportingStructureService = _reportingStructureService.GetReportingStructureByEmployee(employee);

            return Ok(reportingStructureService);
        }


    }
}
