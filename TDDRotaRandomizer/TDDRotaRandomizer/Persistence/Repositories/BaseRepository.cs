using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Persistence.Contexts;

namespace RotaRandomizer.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly RotaDbContext _context;


        public BaseRepository(RotaDbContext context)
        {
            _context = context;
        }

    }
}
