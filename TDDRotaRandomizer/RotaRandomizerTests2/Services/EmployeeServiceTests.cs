using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
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
        private Mock<IEmployeeService> _employeeService;

        public EmployeeServiceTests()
        {            
            _employeeService = new Mock<IEmployeeService>();
        }

        [TestMethod()]
        public async Task ListAsyncTest()
        {
            IEnumerable<Employee> employees = new List<Employee>  {
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
                };
            _employeeService.Setup(mr => mr.ListAsync()).Returns(Task.FromResult(employees));
            List<Employee> result = (await _employeeService.Object.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 10);
        }

        [TestMethod()]
        public void GetEmployeeForShiftTest()
        {
            Assert.Fail();
        }
    }
}