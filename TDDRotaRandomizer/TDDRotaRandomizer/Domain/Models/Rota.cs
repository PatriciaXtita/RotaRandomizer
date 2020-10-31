using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDDRotaRandomizer.Models
{
    public class Rota
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
