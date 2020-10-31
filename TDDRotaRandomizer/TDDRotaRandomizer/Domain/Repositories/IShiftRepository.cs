using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Repositories
{
    public interface IShiftRepository
    {
        Task<IEnumerable<Shift>> ListAsync();
    }
}
