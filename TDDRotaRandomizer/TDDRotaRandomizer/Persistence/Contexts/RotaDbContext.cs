using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Models;

namespace RotaRandomizer.Persistence.Contexts
{
    public class RotaDbContext : DbContext
    {
              
        public RotaDbContext(DbContextOptions<RotaDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
