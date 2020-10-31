using RotaRandomizer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Domain.Repositories
{
    public interface IConfigRepository
    {
        Task<Config> GetAsync(string code);
    }
}
