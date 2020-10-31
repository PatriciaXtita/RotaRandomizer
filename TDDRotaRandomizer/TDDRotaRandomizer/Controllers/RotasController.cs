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
    public class RotasController : ControllerBase
    {
        private readonly RotaServiceInterface _rotaService;

        public RotasController(RotaServiceInterface rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpGet]
        public async Task<IEnumerable<Rota>> GetAllAsync()
        {
            var rotas = await _rotaService.ListAsync();
            return rotas;
        }

    }
}