using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Models
{
    public class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeNumber { get; set; }
        public IList<Shift> WorkingShifts { get; set; } = new List<Shift>();
    }
}
