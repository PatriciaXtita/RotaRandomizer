using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IEmployeeService _employeeService;

        public ShiftService(IShiftRepository shiftRepository, IEmployeeService employeeService)
        {
            _shiftRepository = shiftRepository;
            _employeeService = employeeService;
        }

        public async Task<IEnumerable<Shift>> ListAsync()
        {
            return await _shiftRepository.ListAsync();
        }

        public async Task<IEnumerable<Shift>> CreateShiftsForRota(DateTime start, DateTime end)
        {
            List<Shift> shiftsCreated = new List<Shift>();
            Employee previousShiftEmployee = null;
            List<Employee> employeesWithTwoShifts = new List<Employee>();
            while (start <= end)
            {
                List<DayOfWeek> nonWorkingDays = new List<DayOfWeek>(); //TODO - put nonWorkingDays configurable
                nonWorkingDays.Add(DayOfWeek.Saturday);
                nonWorkingDays.Add(DayOfWeek.Sunday);

                if (!nonWorkingDays.Contains(start.DayOfWeek))
                {
                    //Create 2 shifts
                    Shift morning = new Shift();
                    morning.Start = start;
                    morning.End = start.AddDays(0.5);
                    morning.ShiftType = EShiftType.Morning;
                    Employee morningEmployee = _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts);
                    morning.ShiftEmployee = morningEmployee;
                    previousShiftEmployee = morningEmployee;
                    Shift afternoon = new Shift();
                    afternoon.Start = morning.End;
                    afternoon.End = afternoon.Start.AddDays(0.5);
                    afternoon.ShiftType = EShiftType.Afternoon;
                    Employee afternoonEmployee = _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts);
                    afternoon.ShiftEmployee = afternoonEmployee;
                    previousShiftEmployee = afternoonEmployee;
                    shiftsCreated.Add(morning);                   
                    shiftsCreated.Add(afternoon);
                }
                start = start.AddDays(1);
            }
            //ForEach Shift Assign an Employee to work in it
            setEmployees();

            await _shiftRepository.AddListAsync(shiftsCreated);

            return shiftsCreated;
        }

       
    }
}
