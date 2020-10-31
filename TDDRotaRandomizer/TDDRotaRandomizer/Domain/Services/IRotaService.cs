using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Domain.Services.Communication;
using RotaRandomizer.Models;

namespace RotaRandomizer.Domain.Services
{
    public interface IRotaService
    {
        Task<IEnumerable<Rota>> ListAsync();
        Task<CreateRotaResponse> SaveAsync(Rota rota);
    }
}
