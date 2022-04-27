using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;

namespace Tamplate.DAL.Database
{
    public class TemplateContext : IdentityDbContext
    {

        public TemplateContext(DbContextOptions<TemplateContext> opt) : base(opt)
        {

        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }


    }
}
