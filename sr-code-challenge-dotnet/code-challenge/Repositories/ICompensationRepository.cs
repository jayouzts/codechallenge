﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;


namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetCurrentCompensationByEmployeeId(String id);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}
