﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDDRotaRandomizer.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ShiftTypeEnum ShiftType { get; set; } 
        public Employee ShiftEmployee { get; set; } 
        public Rota Rota { get; set; } 
    }
}
