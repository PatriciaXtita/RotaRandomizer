using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaRandomizer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class RotaRepositoryTests
    {
        private RotaDbContext _context;
        private IRotaRepository _rotaRepository;
        private IUnitOfWork _unitOfWork;


        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
            .UseInMemoryDatabase("TestRotaRepository")
            .Options;
            _context = new RotaDbContext(options);
            _context.Rotas.Add(new Rota { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddDays(14) });
            _context.SaveChanges();
            _rotaRepository = new RotaRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        [TestCleanup]
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod()]
        public async Task ListAsyncTest()
        {
            List<Rota> rotas = (await _rotaRepository.ListAsync()).ToList();
            Assert.AreEqual(1, rotas.Count);
        }

        [TestMethod()]
        public async Task AddAsyncTest()
        {
            Rota rota = new Rota { Id = 3, Start = DateTime.Now.AddMonths(2), End = DateTime.Now.AddMonths(2).AddDays(14) };
            await _rotaRepository.AddAsync(rota);
            await _unitOfWork.CompleteAsync();
            List<Rota> rotas = (await _rotaRepository.ListAsync()).ToList();
            Assert.AreEqual(2, rotas.Count);
        }

        [TestMethod()]
        public async Task FindTest()
        {
            DateTime start = DateTime.Now.AddMonths(-1);
            Rota rota = new Rota { Id = 4, Start = start, End = start.AddDays(-14) };
            await _rotaRepository.AddAsync(rota);
            await _unitOfWork.CompleteAsync();
            Rota foundRota = await _rotaRepository.Find(start);
            Assert.AreEqual(foundRota, rota);
        }

    }
}