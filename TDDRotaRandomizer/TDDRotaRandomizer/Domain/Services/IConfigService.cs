using RotaRandomizer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Services
{
    public interface IConfigService
    {

        Task<Config> GetAsyncRotaDuration();
        Task<Config> GetAsyncRotaStart();
        IEnumerable<DayOfWeek> GetNonWorkingDays();
    }
}
