using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningCoreWeb.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Extension method
            modelBuilder.Seed();

            //Below code converted to Extension method

            //modelBuilder.Entity<Employee>().HasData(
            //    new Employee
            //    {
            //        Id = 1,
            //        Name = "Mary",
            //        Department = DepartmentEnum.IT,
            //        Email = "mary@pragimtech.com"
            //    },
            //    new Employee
            //    {
            //        Id = 2,
            //        Name = "John",
            //        Department = DepartmentEnum.HR,
            //        Email = "john@pragimtech.com"
            //    }
            //);
        }

    }
}
