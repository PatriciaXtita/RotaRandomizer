using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using RotaRandomizer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RotaRandomizerTests.RepositoriesTests
{
    public class EmployeeRepositoryTest
    {

        private readonly EmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeeRepositoryTest()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            IList<Employee> employees = new List<Employee>()
                  {
                   new Employee
                   {
                       Id = 1,
                       Name = "Thomas",
                       EmployeeNumber = "012",
                       WorkingShifts = new List<Shift>()
                    },
                   new Employee
                    {
                       Id = 2,
                       Name = "Arthur",
                       EmployeeNumber = "011",
                       WorkingShifts = new List<Shift>()
                    }
                   };
            mockRepo.Setup(repo => repo.GetAllEmployees()).ReturnsAsync(employees);
        }

    }
}
