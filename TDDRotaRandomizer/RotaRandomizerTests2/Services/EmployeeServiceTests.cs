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


        [TestInitialize]
        public void InitializeTest()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
        .UseInMemoryDatabase("EmployeeServiceTest")
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

        [TestCleanup]
        public void DisposeTest()
        {
            _context.Database.EnsureDeleted();
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
            List<Employee> employeesWithZeroShifts = new List<Employee>();
            Employee chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts, employeesWithZeroShifts);
            Assert.IsNull(chosen);
        }

        [TestMethod()]
        public async Task GetEmployeeForShiftOnlyOneEmployeeTest()
        {
            Employee previousShiftEmployee = null;
            List<Employee> employeesWithTwoShifts = (await _employeeService.ListAsync()).ToList();
            Employee shouldBePicked = employeesWithTwoShifts.First();
            employeesWithTwoShifts.Remove(shouldBePicked);
            List<Employee> employeesWithZeroShifts = new List<Employee>();
            Employee chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts, employeesWithZeroShifts);
            Assert.AreEqual(shouldBePicked, chosen);
        }

        [TestMethod()]
        public async Task GetEmployeeForShiftWithPreviousBeingAddedToTwoShiftsEmployeesTest()
        {
            Employee previousShiftEmployee = null;
            List<Employee> employeesWithTwoShifts = new List<Employee>();
            int totalEmployees = (await _employeeService.ListAsync()).ToList().Count;
            List<Employee> employeesWithZeroShifts = new List<Employee>();
            Employee chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts, employeesWithZeroShifts);
            while (chosen != null)
            {
                employeesWithTwoShifts.Add(chosen);
                chosen = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts, employeesWithZeroShifts);
            }
            Assert.AreEqual(totalEmployees, employeesWithTwoShifts.Count);
        }

    }
}