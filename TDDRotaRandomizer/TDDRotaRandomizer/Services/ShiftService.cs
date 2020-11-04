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
        private readonly IConfigService _configService;

        public ShiftService(IShiftRepository shiftRepository, IEmployeeService employeeService, IConfigService configService)
        {
            _shiftRepository = shiftRepository;
            _employeeService = employeeService;
            _configService = configService;
        }

        public async Task<IEnumerable<Shift>> GetShiftsInDay(DateTime date)
        {
            return await _shiftRepository.GetShiftsInDay(date);
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
            List<Employee> employeesWithZeroShifts = (await _employeeService.ListAsync()).ToList();
            while (start <= end)
            {
                List<DayOfWeek> nonWorkingDays = _configService.GetNonWorkingDays().ToList();

                if (!nonWorkingDays.Contains(start.DayOfWeek))
                {
                    Shift morning = new Shift();
                    morning.Start = start;
                    morning.End = start.AddDays(0.5);
                    morning.ShiftType = EShiftType.Morning;
                    Employee morningEmployee = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts, employeesWithZeroShifts);
                    employeesWithZeroShifts.Remove(morningEmployee);
                    morning.ShiftEmployee = morningEmployee;
                    previousShiftEmployee = morningEmployee;
                    if (shiftsCreated.Select(e => e.ShiftEmployee).ToList().Contains(morningEmployee))
                        employeesWithTwoShifts.Add(morningEmployee);
                    shiftsCreated.Add(morning);

                    Shift afternoon = new Shift();
                    afternoon.Start = morning.End;
                    afternoon.End = afternoon.Start.AddDays(0.5);
                    afternoon.ShiftType = EShiftType.Afternoon;
                    Employee afternoonEmployee = await _employeeService.GetEmployeeForShift(previousShiftEmployee, employeesWithTwoShifts, employeesWithZeroShifts);
                    afternoon.ShiftEmployee = afternoonEmployee;
                    employeesWithZeroShifts.Remove(afternoonEmployee);
                    previousShiftEmployee = afternoonEmployee;
                    if (shiftsCreated.Select(e => e.ShiftEmployee).ToList().Contains(afternoonEmployee))
                        employeesWithTwoShifts.Add(afternoonEmployee);
                    shiftsCreated.Add(afternoon);
                }
                start = start.AddDays(1);
            }
            await _shiftRepository.AddListAsync(shiftsCreated);
            return shiftsCreated;
        }


    }
}
