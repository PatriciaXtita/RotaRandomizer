using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Models;

namespace RotaRandomizer.Domain.Services
{
    public interface IRotaService
    {
        Task<IEnumerable<Rota>> ListAsync();
    }
}
