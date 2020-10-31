using Microsoft.EntityFrameworkCore;
using RotaRandomizer.Domain.Models;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Persistence.Repositories
{
    public class ConfigRepository : BaseRepository, IConfigRepository
    {
        public ConfigRepository(RotaDbContext context) : base(context) { }

        public async Task<Config> GetAsync(string code)
        {
            return await _context.Configurations.Where(c => c.Code.Equals(code)).FirstOrDefaultAsync();
        }

    }
}
