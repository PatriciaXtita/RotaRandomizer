using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _employeeRepository.ListAsync();
        }
    }
}
