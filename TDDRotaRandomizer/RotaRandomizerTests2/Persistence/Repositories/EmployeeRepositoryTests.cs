using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace RotaRandomizer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class EmployeeRepositoryTests
    {
        private RotaDbContext _context;
        private IEmployeeRepository _employeeRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
                        .UseInMemoryDatabase("TestEmployeeRepository")
                        .Options;
            _context = new RotaDbContext(options);
            _context.Employees.Add(new Employee { Id = 1, Name = "John", EmployeeNumber = "E123" });
            _context.Employees.Add(new Employee { Id = 2, Name = "Jane", EmployeeNumber = "E234" });
            _context.SaveChanges();
            _employeeRepository = new EmployeeRepository(_context);
        }

        [TestCleanup]
        public void DisposeTest()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod()]
        public async Task ListAsyncTest()
        {
            List<Employee> employees = (await _employeeRepository.ListAsync()).ToList();
            Assert.AreEqual(2, employees.Count);
            Assert.AreEqual("John", employees[0].Name);
            Assert.AreEqual("Jane", employees[1].Name);
        }
    }
}