using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Services
{
    public interface IShiftService
    {
        Task<IEnumerable<Shift>> ListAsync();
        Task<IEnumerable<Shift>> CreateShiftsForRota(DateTime beginningOfRotaDay, DateTime endOfRotaDay);
    }
}
