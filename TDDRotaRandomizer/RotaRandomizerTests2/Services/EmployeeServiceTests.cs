using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using RotaRandomizer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaRandomizer.Services.Tests
{
    [TestClass()]
    public class EmployeeServiceTests
    {
        private RotaDbContext _context;
        private IEmployeeRepository _employeeRepository;
        private IEmployeeService _employeeService;

        [ClassInitialize]
        public void ClassSetUp()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
               .UseInMemoryDatabase("RotaRandomizer")
               .Options;
            _context = new RotaDbContext(options);
            _context.Employees.Add(new Employee { Id = 1, Name = "John", EmployeeNumber = "E123" });
            _context.Employees.Add(new Employee { Id = 2, Name = "Jane", EmployeeNumber = "E234" });
            _context.Employees.Add(new Employee { Id = 3, Name = "Jamie", EmployeeNumber = "E345" });
            _context.Employees.Add(new Employee { Id = 4, Name = "Janette", EmployeeNumber = "E456" });
            _context.Employees.Add(new Employee { Id = 5, Name = "Joshua", EmployeeNumber = "E567" });
            _context.Employees.Add(new Employee { Id = 6, Name = "Jack", EmployeeNumber = "E678" });
            _context.Employees.Add(new Employee { Id = 7, Name = "Jeremy", EmployeeNumber = "E789" });
            _context.Employees.Add(new Employee { Id = 8, Name = "Jennifer", EmployeeNumber = "E890" });
            _context.Employees.Add(new Employee { Id = 9, Name = "Jasper", EmployeeNumber = "E901" });
            _context.Employees.Add(new Employee { Id = 10, Name = "Joselyn", EmployeeNumber = "E012" });
            _context.SaveChanges();
            _employeeRepository = new EmployeeRepository(_context);
            _employeeService = new EmployeeService(_employeeRepository);
        }


        [ClassInitialize]
        public void TearDown()
        {
            _context.Dispose();
        }


        [TestMethod()]
        public async Task ListAsyncTest()
        {
            List<Employee> result = (await _employeeService.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 10);
            Assert.AreEqual(result.First().Name, "John");
        }

        [TestMethod()]
        public async Task GetEmployeeForShiftNoEmployeeTest()
        {
            Employee previousShiftEmployee = null;
            List<Employee> employeesWithTwoShifts = (await _employeeService.ListAsync()).ToList();
            Employee chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts);
            Assert.IsNull(chosen);
        }

        [TestMethod()]
        public async Task GetEmployeeForShiftOnlyOneEmployeeTest()
        {
            Employee previousShiftEmployee = null;
            List<Employee> employeesWithTwoShifts = (await _employeeService.ListAsync()).ToList();
            Employee shouldBePicked = employeesWithTwoShifts.First();
            employeesWithTwoShifts.Remove(shouldBePicked);
            Employee chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts);
            Assert.AreEqual(shouldBePicked, chosen);
        }

        [TestMethod()]
        public async Task GetEmployeeForShiftWithPreviousBeingAddedToTwoShiftsEmployeesTest()
        {
            Employee previousShiftEmployee = null;
            List<Employee> employeesWithTwoShifts = new List<Employee>();
            int totalEmployees = (await _employeeService.ListAsync()).ToList().Count;
            Employee chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts);
            while (chosen != null)
            {
                employeesWithTwoShifts.Add(chosen);
                chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts);                
            }
            Assert.AreEqual(totalEmployees, employeesWithTwoShifts.Count);
        }

    }
}