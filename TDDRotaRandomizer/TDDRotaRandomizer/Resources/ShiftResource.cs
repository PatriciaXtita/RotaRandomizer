using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Resources
{
    public class ShiftResource
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ShiftType { get; set; }
        public string ShiftEmployee { get; set; }
    }
}
