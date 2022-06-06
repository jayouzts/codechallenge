using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {

        private readonly ILogger<EmployeeService> _logger;
        private readonly IReportingStructureRepository _reportingStructureRepository;
        public ReportingStructureService(ILogger<EmployeeService> logger, IReportingStructureRepository reportingStructureRepository)
        {
        
            _logger = logger;
            _reportingStructureRepository = reportingStructureRepository;
        }
        public ReportingStructure GetReportingStructureByEmployee(Employee employee)
        {
            if (employee != null)
            {

             
                return _reportingStructureRepository.GetByEmployee(employee);



            }

            return null;
        }
    }
}
