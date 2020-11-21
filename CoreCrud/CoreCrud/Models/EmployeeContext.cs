using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCrud.Models
{
    public class EmployeeContext: DbContext
    {
        //Constructor gets initialised by Dependency Injection
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {

        }

        //Add Employee class to DBModel
        public DbSet<Employee> Employees { get; set; }
    }
}
