using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;

namespace RotaRandomizer.Services
{
    public class RotaService : RotaServiceInterface
    {
        private readonly RotaRepositoryInterface _rotaRepository;

        public RotaService(RotaRepositoryInterface rotaRepository)
        {
            this._rotaRepository = rotaRepository;
        }

        public async Task<IEnumerable<Rota>> ListAsync()
        {
            return await _rotaRepository.ListAsync();
        }
    }
}
