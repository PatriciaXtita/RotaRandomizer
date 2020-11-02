using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ShiftServiceTests
    {

        private RotaDbContext _context;
        private IShiftRepository _shiftRepository;
        private IShiftService _shiftService;


        [TestInitialize]
        public void InitializeTest()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
        .UseInMemoryDatabase("ShiftServiceTest")
        .Options;
            _context = new RotaDbContext(options);
            _context.Shifts.Add(new Shift { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(8), ShiftType = EShiftType.Morning });
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
            EmployeeRepository employeeRepository = new EmployeeRepository(_context);
            EmployeeService employeeService = new EmployeeService(employeeRepository);
            ConfigRepository configRepository = new ConfigRepository(_context);
            ConfigService configService = new ConfigService(configRepository);
            _shiftRepository = new ShiftRepository(_context);
            _shiftService = new ShiftService(_shiftRepository, employeeService, configService);
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
            List<Shift> result = (await _shiftService.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod()]
        public async Task CreateShiftsForRotaTest()
        {
            List<Shift> shifts = (await _shiftService.CreateShiftsForRota(DateTime.Parse("2020-11-02 00:00:00"), DateTime.Parse("2020-11-13 00:00:00"))).ToList();
            EveryShiftHasEmployee(shifts);
            AllEmployeesHaveTwoShifts(shifts);
            EmployeesOnlyWorkOneShiftADay(shifts);
            EmployeesDoNotWorkTwoShiftsInARow(shifts);

        }

        private void EveryShiftHasEmployee(IEnumerable<Shift> shifts)
        {
            foreach (Shift shift in shifts)
            {
                if (shift.ShiftEmployee == null)
                {
                    Assert.Fail();
                }
            }
        }

        private void EmployeesDoNotWorkTwoShiftsInARow(IEnumerable<Shift> shifts)
        {
            Shift previous = null;
            foreach (Shift shift in shifts)
            {
                if (previous != null)
                {
                    if (previous.ShiftEmployee.Equals(shift.ShiftEmployee))
                    {
                        Assert.Fail();
                    }
                }
                previous = shift;
            }
        }

        private void EmployeesOnlyWorkOneShiftADay(IEnumerable<Shift> shifts)
        {
            List<Employee> rotaEmployees = shifts.Select(s => s.ShiftEmployee).Distinct().ToList();
            foreach (Employee employee in rotaEmployees)
            {
                foreach (Shift workingShift in employee.WorkingShifts)
                {
                    List<Shift> employeeShifts = employee.WorkingShifts.ToList();
                    employeeShifts.Remove(workingShift);
                    IsNotSameDay(workingShift, employeeShifts);
                }
            }
        }

        private void IsNotSameDay(Shift workingShift, List<Shift> shifts)
        {
            foreach (Shift shift in shifts)
            {
                if (workingShift.Start.Date.Equals(shift.Start.Date))
                {
                    Assert.Fail();
                }
            }
        }

        private void AllEmployeesHaveTwoShifts(IEnumerable<Shift> shifts)
        {
            List<Employee> rotaEmployees = shifts.Select(s => s.ShiftEmployee).Distinct().ToList();
            foreach (Employee employee in rotaEmployees)
            {
                if (shifts.Where(s => s.ShiftEmployee.Equals(employee)).Count() != 2)
                {
                    Assert.Fail();
                }
            }
        }

    }
}