using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Models;

namespace RotaRandomizer.Domain.Repositories
{
    public interface RotaRepositoryInterface
    {
        Task<IEnumerable<Rota>> ListAsync();
    }
}
