using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Models;
using BAL.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace BAL.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public DatabaseContext context
        {
            get
            {
                return db as DatabaseContext;
            }
        }

        public CategoryRepository(DbContext db)
        {
            this.db = db;
        }

    }
}
