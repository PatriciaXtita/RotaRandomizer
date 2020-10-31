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

        public async Task<Employee> GetEmployeeForShift(Employee previousShiftEmployee, IEnumerable<Employee> employeesWithTwoShifts)
        {
            List<Employee> employees = (await _employeeRepository.GetAllEmployees()).ToList();
            employees.Remove(previousShiftEmployee);
            employees.RemoveAll(e => employeesWithTwoShifts.Contains(e));
            var random = new Random();
            int index = random.Next(employees.Count);
            return employees.ElementAt(index);

        }

    }
}
