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

        public async Task AddAsync(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
        }


        public async Task AddListAsync(IEnumerable<Shift> shifts)
        {
            foreach (Shift shift in shifts)
            {
                await AddAsync(shift);
            }
        }

        public async Task<IEnumerable<Shift>> GetShiftsInDay(DateTime date)
        {
            return await _context.Shifts.Where(s => s.Start.Date.Equals(date.Date)).Include(s => s.ShiftEmployee).OrderBy(s => s.Start).ToListAsync();
        }
        
        public async Task<IEnumerable<Shift>> ListAsync()
        {
            return await _context.Shifts.ToListAsync();
        }
    }
}
