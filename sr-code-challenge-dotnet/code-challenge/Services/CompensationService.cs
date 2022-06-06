using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Repositories;
using Microsoft.Extensions.Logging;
using challenge.Models;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public CompensationService(ILogger<EmployeeService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            _compensationRepository.Add(compensation);
            _compensationRepository.SaveAsync().Wait();
            return compensation;

        }

        public Compensation GetCurrentSalaryByEmployeeId(string id)
        {
           if (!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetCurrentCompensationByEmployeeId(id);
            }

            return null;
        }
    }
}
