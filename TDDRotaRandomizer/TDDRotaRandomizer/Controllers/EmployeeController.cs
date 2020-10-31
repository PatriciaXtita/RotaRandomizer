using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;

namespace RotaRandomizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _employeeService.ListAsync();
            return employees;
        }
    }
}