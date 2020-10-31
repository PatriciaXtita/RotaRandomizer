using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaRandomizer.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public async Task<IEnumerable<Shift>> ListAsync()
        {
            return await _shiftRepository.ListAsync();
        }

    }
}
