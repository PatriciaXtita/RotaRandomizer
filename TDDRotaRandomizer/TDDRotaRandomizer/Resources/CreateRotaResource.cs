using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Resources
{
    public class CreateRotaResource
    {
        [Required]
        public DateTime Date { get; set; }
    }
}
