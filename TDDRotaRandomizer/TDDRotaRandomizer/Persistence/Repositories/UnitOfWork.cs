using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RotaDbContext _context;

        public UnitOfWork(RotaDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
