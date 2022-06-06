using challenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Compensation> Compensations { get; set; }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
                .HasOne<Compensation>(c => c.Compensation)
                .WithOne(e => e.Employee)
                .HasForeignKey<Compensation>(c => c.EmployeeId);

        }



    }
}
