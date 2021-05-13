using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetIdentityProject.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Rachit: Add this otherwise we would get exception during migration for IdentityDbContext
            //Keys of Identity tables are mapped in OnModelCreating method of IdentityDbContext class
            base.OnModelCreating(modelBuilder);            
        }
    }
}
