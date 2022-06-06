using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Extensions;

namespace challenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {

    
        public ReportingStructure GetByEmployee(Employee employee)
        {

            if (employee == null)
                throw new Exception("Invalid Employee");

            var subords = employee.DirectReports.Flatten<Employee>(e => e.DirectReports).ToList();

            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = subords.Count()

            
            };

        }

       


    }
}
