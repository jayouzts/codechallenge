using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace challenge.Models
{
    public class Compensation
    {
        public string CompensationId { get; set; }
       
        public string EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee {get; set;}

        public Decimal Salary { get; set; }

        public DateTime EffectiveDate { get; set; }

    }
}

