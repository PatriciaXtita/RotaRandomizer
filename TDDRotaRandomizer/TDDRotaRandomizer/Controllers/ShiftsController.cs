using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;

namespace RotaRandomizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftService _shiftService;

        public ShiftsController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public async Task<IEnumerable<Shift>> GetAllAsync()
        {
            var shifts = await _shiftService.ListAsync();
            return shifts;
        }
    }
}