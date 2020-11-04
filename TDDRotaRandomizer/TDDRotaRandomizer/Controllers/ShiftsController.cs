using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Models;
using RotaRandomizer.Resources;

namespace RotaRandomizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        private readonly IMapper _mapper;

        public ShiftsController(IShiftService shiftService, IMapper mapper)
        {
            _shiftService = shiftService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ShiftResource>> GetAllAsync()
        {
            var shifts = await _shiftService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftResource>>(shifts);
            return resources;
        }

        [HttpGet]
        public async Task<IEnumerable<ShiftResource>> GetShiftsInDay(DateTime date)
        {
            var shifts = await _shiftService.GetShiftsInDay(date);
            var resources = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftResource>>(shifts);
            return resources;
        }
    }
}