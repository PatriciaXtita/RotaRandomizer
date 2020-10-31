using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Extensions;
using RotaRandomizer.Models;
using RotaRandomizer.Resources;

namespace RotaRandomizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotasController : ControllerBase
    {
        private readonly IRotaService _rotaService;
        private readonly IMapper _mapper;

        public RotasController(IRotaService rotaService, IMapper mapper)
        {
            _rotaService = rotaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<RotaResource>> GetAllAsync()
        {
            var rotas = await _rotaService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Rota>, IEnumerable<RotaResource>>(rotas);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateRotaResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var rota = _mapper.Map<CreateRotaResource, Rota>(resource);
            var result = await _rotaService.SaveAsync(rota);

            if (!result.Success)
                return BadRequest(result.Message);

            var rotaResource = _mapper.Map<Rota, RotaResource>(result.Rota);
            return Ok(rotaResource);

        }

    }
}