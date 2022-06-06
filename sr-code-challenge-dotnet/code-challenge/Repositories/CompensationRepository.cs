using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {

        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

   
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetCurrentCompensationByEmployeeId(string id)
        {
            return _employeeContext.Compensations
                  .Include(c => c.Employee)
                  .OrderByDescending(c => c.EffectiveDate)
                  .FirstOrDefault(x => x.EmployeeId == id && x.EffectiveDate <= DateTime.Now);

        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}
