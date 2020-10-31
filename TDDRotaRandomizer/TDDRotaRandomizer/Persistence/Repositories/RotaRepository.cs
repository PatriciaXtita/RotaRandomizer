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
    public class RotaRepository : BaseRepository, RotaRepositoryInterface
    {
        public RotaRepository(RotaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Rota>> ListAsync()
        {
            return await _context.Rotas.ToListAsync();
        }

    }
}
