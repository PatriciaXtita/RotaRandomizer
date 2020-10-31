using Microsoft.EntityFrameworkCore;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Persistence.Repositories
{
    public class ShiftRepository : BaseRepository, IShiftRepository
    {
        public ShiftRepository(RotaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Shift>> ListAsync()
        {
            return await _context.Shifts.ToListAsync();
        }
    }
}
