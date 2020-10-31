using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
using RotaRandomizer.Resources;

namespace RotaRandomizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeResource>> GetAllAsync()
        {
            var employees = await _employeeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);
            return resources;
        }
    }
}