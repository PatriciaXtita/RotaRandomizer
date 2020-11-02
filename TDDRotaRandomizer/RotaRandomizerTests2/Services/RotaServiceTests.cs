using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RotaRandomizer.Domain.Models;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Domain.Services.Communication;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using RotaRandomizer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RotaRandomizer.Services.Tests
{
    [TestClass()]
    public class RotaServiceTests
    {
        private RotaDbContext _context;
        private IRotaRepository _rotaRepository;
        private RotaService _rotaService;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void InitializeTest()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
        .UseInMemoryDatabase("RotaServiceTest")
        .Options;
            _context = new RotaDbContext(options);
            _context.Rotas.Add(new Rota { Id = 1, Start = DateTime.Parse("2020-10-05 00:00:00"), End = DateTime.Parse("2020-10-16 00:00:00") });
            _context.Configurations.Add(new Config { Id = 1, Description = "Day of week where rota starts", Code = "RotaStart", Value = 1 });
            _context.Configurations.Add(new Config { Id = 2, Description = "Rota duration in working days", Code = "RotaDuration", Value = 10 });
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
            _rotaRepository = new RotaRepository(_context);
            EmployeeRepository employeeRepository = new EmployeeRepository(_context);
            EmployeeService employeeService = new EmployeeService(employeeRepository);
            ShiftRepository shiftRepository = new ShiftRepository(_context);
            ConfigRepository configRepository = new ConfigRepository(_context);
            ConfigService configService = new ConfigService(configRepository);
            IShiftService shiftService = new ShiftService(shiftRepository, employeeService, configService);
            _unitOfWork = new UnitOfWork(_context);
            _rotaService = new RotaService(_rotaRepository, shiftService, _unitOfWork, configService);
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
            List<Rota> result = (await _rotaService.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 1);
        }



        [TestMethod()]
        public async Task GetRotaEndTest()
        {
            DateTime mondayDate = DateTime.Parse("2020-11-02 00:00:00");
            DateTime tuesdayDate = DateTime.Parse("2020-11-03 00:00:00");
            DateTime wednesdayDate = DateTime.Parse("2020-11-04 00:00:00");
            DateTime thursdayDate = DateTime.Parse("2020-11-05 00:00:00");
            DateTime fridayDate = DateTime.Parse("2020-11-06 00:00:00");

            List<DateTime> dates = new List<DateTime>();
            dates.Add(mondayDate);
            dates.Add(tuesdayDate);
            dates.Add(wednesdayDate);
            dates.Add(thursdayDate);
            dates.Add(fridayDate);

            Assert.AreEqual(await _rotaService.GetRotaEnd(mondayDate), DateTime.Parse("2020-11-13 00:00:00"));
            Assert.AreEqual(await _rotaService.GetRotaEnd(tuesdayDate), DateTime.Parse("2020-11-16 00:00:00"));
            Assert.AreEqual(await _rotaService.GetRotaEnd(wednesdayDate), DateTime.Parse("2020-11-17 00:00:00"));
            Assert.AreEqual(await _rotaService.GetRotaEnd(thursdayDate), DateTime.Parse("2020-11-18 00:00:00"));
            Assert.AreEqual(await _rotaService.GetRotaEnd(fridayDate), DateTime.Parse("2020-11-19 00:00:00"));

        }

        [TestMethod()]
        public void GetRotaStartTest()
        {
            DateTime mondayDate = DateTime.Parse("2020-11-02 00:00:00");
            DateTime tuesdayDate = DateTime.Parse("2020-11-03 00:00:00");
            DateTime wednesdayDate = DateTime.Parse("2020-11-04 00:00:00");
            DateTime thursdayDate = DateTime.Parse("2020-11-05 00:00:00");
            DateTime fridayDate = DateTime.Parse("2020-11-06 00:00:00");

            List<DateTime> dates = new List<DateTime>();
            dates.Add(mondayDate);
            dates.Add(tuesdayDate);
            dates.Add(wednesdayDate);
            dates.Add(thursdayDate);
            dates.Add(fridayDate);

            AssertChosenIsReceived(DayOfWeek.Monday, dates);
            AssertChosenIsReceived(DayOfWeek.Tuesday, dates);
            AssertChosenIsReceived(DayOfWeek.Wednesday, dates);
            AssertChosenIsReceived(DayOfWeek.Thursday, dates);
            AssertChosenIsReceived(DayOfWeek.Friday, dates);
        }


        public void AssertChosenIsReceived(DayOfWeek chosen, IEnumerable<DateTime> dates)
        {
            foreach (DateTime date in dates)
            {
                Assert.AreEqual(_rotaService.GetRotaStart(date, chosen).DayOfWeek, chosen);
            }
        }

        [TestMethod()]
        public async Task SaveAsyncSimpleTest()
        {
            Rota r = new Rota { Start = DateTime.Parse("2020-11-02 00:00:00") };
            CreateRotaResponse response = await _rotaService.SaveAsync(r);
            Assert.AreEqual(response.Rota.Start, DateTime.Parse("2020-11-02 00:00:00"));
            Assert.AreEqual(response.Rota.End, DateTime.Parse("2020-11-13 00:00:00"));
            Assert.AreEqual(response.Rota.Shifts.Count(), 20);
            Assert.AreEqual(response.Success, true);
        }

        [TestMethod()]
        public async Task SaveAsyncRepeatedRotaTest()
        {
            Rota r = new Rota { Start = DateTime.Parse("2020-11-02 00:00:00") };
            CreateRotaResponse response = await _rotaService.SaveAsync(r);
            Assert.AreEqual(response.Success, true);
            CreateRotaResponse secondResponse = await _rotaService.SaveAsync(r);
            Assert.AreEqual(secondResponse.Success, false);
        }

        [TestMethod()]
        public async Task SaveAsyncDateInBetweenRotaTest()
        {
            DateTime mondayDate = DateTime.Parse("2020-11-02 00:00:00");
            DateTime wednesdayDate = DateTime.Parse("2020-11-04 00:00:00");
            CreateRotaResponse response1 = await _rotaService.SaveAsync(new Rota { Start = mondayDate });
            CreateRotaResponse response2 = await _rotaService.SaveAsync(new Rota { Start = wednesdayDate });
            Rota created = response1.Rota;
            Assert.IsTrue(response1.Success);
            Assert.IsFalse(response2.Success);
        }


        [TestMethod()]
        public async Task SaveAsyncRotaRequirementsTest()
        {
            DateTime startOfTheYear = DateTime.Parse("2020-01-01 00:00:00");
            DateTime endOfTheYear = DateTime.Parse("2020-12-31 00:00:00");
            Random rnd = new Random();
            while (startOfTheYear <= endOfTheYear)
            {
                int daysToSum = rnd.Next(1, 5);
                startOfTheYear = startOfTheYear.AddDays(daysToSum);
                await VerifyRotaRequirements(startOfTheYear);
            }
        }

        private async Task VerifyRotaRequirements(DateTime date)
        {
            CreateRotaResponse response1 = await _rotaService.SaveAsync(new Rota { Start = date });
            Rota rota = response1.Rota;
            EveryShiftHasEmployee(rota);
            RotaDurationIsTenWorkingDays(rota);
            AllEmployeesHaveTwoShifts(rota);
            EmployeesOnlyWorkOneShiftADay(rota);
            EmployeesDoNotWorkTwoShiftsInARow(rota);
        }

        private void EmployeesDoNotWorkTwoShiftsInARow(Rota rota)
        {
            Shift previous = null;
            foreach (Shift shift in rota.Shifts.ToList())
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

        private void EmployeesOnlyWorkOneShiftADay(Rota rota)
        {
            List<Employee> rotaEmployees = rota.Shifts.Select(s => s.ShiftEmployee).Distinct().ToList();
            foreach (Employee employee in rotaEmployees)
            {
                foreach (Shift workingShift in employee.WorkingShifts)
                {
                    List<Shift> shifts = employee.WorkingShifts.ToList();
                    shifts.Remove(workingShift);
                    IsNotSameDay(workingShift, shifts);
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

        private void AllEmployeesHaveTwoShifts(Rota rota)
        {
            List<Employee> rotaEmployees = rota.Shifts.Select(s => s.ShiftEmployee).Distinct().ToList();
            foreach (Employee employee in rotaEmployees)
            {
                if (rota.Shifts.Where(s => s.ShiftEmployee.Equals(employee)).Count() != 2)
                {
                    Assert.Fail();
                }
            }
        }

        private void RotaDurationIsTenWorkingDays(Rota rota)
        {
            DateTime start = rota.Start;
            DateTime end = rota.End;
            int duration = 0;
            while (start <= end)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                    duration++;
                start = start.AddDays(1);
            }
            if (duration != 10)
                Assert.Fail();
        }

        private void EveryShiftHasEmployee(Rota rota)
        {
            foreach (Shift shift in rota.Shifts)
            {
                if (shift.ShiftEmployee == null)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod()]
        public async Task InBetweenRotaAsyncTest()
        {
            Rota rota = new Rota { Start = DateTime.Parse("2020-11-02 00:00:00") };
            CreateRotaResponse response = await _rotaService.SaveAsync(rota);
            Rota repeatedRota = await _rotaService.InBetweenRotaAsyncTest(DateTime.Parse("2020-11-04 00:00:00"));
            Assert.AreEqual(response.Rota, repeatedRota);
        }
    }
}