using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;

namespace RotaRandomizer.Persistence.Repositories
{
    public class RotaRepository : BaseRepository, IRotaRepository
    {
        public RotaRepository(RotaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rota>> ListAsync()
        {
            return await _context.Rotas.Include(s => s.Shifts).ThenInclude(s => s.ShiftEmployee).ToListAsync();
        }

        public async Task AddAsync(Rota rota)
        {

            await _context.Rotas.AddAsync(rota);
        }


        public async Task<Rota> Find(DateTime start)
        {
            if(_context.Rotas.Where(r => r.Start.Equals(start)).Any())
            {
                return await _context.Rotas.Where(r => r.Start.Equals(start)).FirstAsync();
            }
            else
            {
                return null;
            }
        }
    }
}
