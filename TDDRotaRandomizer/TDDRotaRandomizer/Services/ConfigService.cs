using RotaRandomizer.Domain.Models;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigRepository _configRepository;

        public ConfigService(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<Config> GetAsyncRotaDuration()
        {
            return await _configRepository.GetAsync("RotaDuration");
        }

        public async Task<Config> GetAsyncRotaStart()
        {
            return await _configRepository.GetAsync("RotaStart");
        }

        public IEnumerable<DayOfWeek> GetNonWorkingDays()
        {
            List<DayOfWeek> result = new List<DayOfWeek>(); 
            result.Add(DayOfWeek.Saturday);
            result.Add(DayOfWeek.Sunday);
            return result;
        }

    }
}
