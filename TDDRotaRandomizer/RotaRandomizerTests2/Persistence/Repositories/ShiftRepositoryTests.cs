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
    public class ShiftRepositoryTests
    {
        private RotaDbContext _context;
        private IShiftRepository _shiftRepository;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void InitializeTest()
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
              .UseInMemoryDatabase("TestShiftRepository")
              .Options;
            _context = new RotaDbContext(options);
            _context.Shifts.Add(new Shift { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(8), ShiftType = EShiftType.Afternoon });
            _context.SaveChanges();
            _shiftRepository = new ShiftRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        [TestCleanup]
        public void DisposeTest()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod()]
        public async Task ListAsyncTest()
        {
            List<Shift> shifts = (await _shiftRepository.ListAsync()).ToList();
            Assert.AreEqual(1, shifts.Count);
        }

        [TestMethod()]
        public async Task AddAsyncTest()
        {
            Shift shift = new Shift { Id = 2, Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).AddHours(8), ShiftType = EShiftType.Morning };
            await _shiftRepository.AddAsync(shift);
            await _unitOfWork.CompleteAsync();
            List<Shift> shifts = (await _shiftRepository.ListAsync()).ToList();
            Assert.AreEqual(2, shifts.Count);
        }

        [TestMethod()]
        public async Task AddListAsyncTest()
        {
            //Insert shifts list
            IEnumerable<Shift> shifts = new List<Shift>
                {
                    new Shift { Id = 2, Start = DateTime.Now, End = DateTime.Now.AddHours(8), ShiftType = EShiftType.Afternoon },
                    new Shift { Id = 3, Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).AddHours(8), ShiftType = EShiftType.Afternoon },
                    new Shift { Id = 4, Start = DateTime.Now.AddDays(2), End = DateTime.Now.AddDays(2).AddHours(8), ShiftType = EShiftType.Afternoon },
                };
            await _shiftRepository.AddListAsync(shifts);
            await _unitOfWork.CompleteAsync();
            List<Shift> shiftsAfterInsert = (await _shiftRepository.ListAsync()).ToList();
            Assert.AreEqual(4, shiftsAfterInsert.Count);
        }


    }
}