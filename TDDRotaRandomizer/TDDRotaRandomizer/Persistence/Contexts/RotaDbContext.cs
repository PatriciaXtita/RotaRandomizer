using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDDRotaRandomizer.Models;

namespace TDDRotaRandomizer.Persistence.Contexts
{
    public class RotaDbContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public RotaDbContext(DbContextOptions<RotaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
