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

        /// <summary>
        /// Get all shifts in the system
        /// </summary> 
        [HttpGet]
        public async Task<IEnumerable<ShiftResource>> GetAllAsync()
        {
            var shifts = await _shiftService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftResource>>(shifts);
            return resources;
        }


        /// <summary>
        /// Get today's shifts
        /// </summary> 
        [HttpGet]
        [Route("today")]
        public async Task<IEnumerable<ShiftResource>> GetTodaysShift()
        {
            var shifts = await _shiftService.GetTodaysShift();
            var resources = _mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftResource>>(shifts);
            return resources;
        }
    }
}