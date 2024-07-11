using Microsoft.EntityFrameworkCore;
using MTechTest.Models;

namespace MTechTest.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) 
            : base(options)
        { 
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
