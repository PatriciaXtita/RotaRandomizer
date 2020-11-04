using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Repositories
{
    public interface IShiftRepository
    {
        
        Task<IEnumerable<Shift>> GetShiftsInDay(DateTime date);
        Task<IEnumerable<Shift>> ListAsync();
        Task AddAsync(Shift morning);
        Task AddListAsync(IEnumerable<Shift> shiftsCreated);
    }
}
