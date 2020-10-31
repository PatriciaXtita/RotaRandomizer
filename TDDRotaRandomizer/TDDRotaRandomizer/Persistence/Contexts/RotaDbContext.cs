using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Models;
using RotaRandomizer.Domain.Models;

namespace RotaRandomizer.Persistence.Contexts
{
    public class RotaDbContext : DbContext
    {

        public RotaDbContext(DbContextOptions<RotaDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Config> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().HasData
           (
               new Employee { Id = 1, Name = "John", EmployeeNumber = "E123" },
               new Employee { Id = 2, Name = "Jane", EmployeeNumber = "E234" },
               new Employee { Id = 3, Name = "Jamie", EmployeeNumber = "E345" },
               new Employee { Id = 4, Name = "Janette", EmployeeNumber = "E456" },
               new Employee { Id = 5, Name = "Joshua", EmployeeNumber = "E567" },
               new Employee { Id = 6, Name = "Jack", EmployeeNumber = "E678" },
               new Employee { Id = 7, Name = "Jeremy", EmployeeNumber = "E789" },
               new Employee { Id = 8, Name = "Jennifer", EmployeeNumber = "E890" },
               new Employee { Id = 9, Name = "Jasper", EmployeeNumber = "E901" },
               new Employee { Id = 10, Name = "Joselyn", EmployeeNumber = "E012" }
           );

            builder.Entity<Config>().HasData
           (
               new Config { Id = 1, Description = "Day of week where rota starts", Value = (double) DayOfWeek.Monday, Code = "RotaStart" }, 
               new Config { Id = 2, Description = "Rota duration in working days", Value = 10, Code = "RotaDuration" }
           );

        }


    }
}
