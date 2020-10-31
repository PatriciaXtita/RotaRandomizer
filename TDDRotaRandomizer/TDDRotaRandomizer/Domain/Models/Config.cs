using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Models
{
    public class Config
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
    }
}
