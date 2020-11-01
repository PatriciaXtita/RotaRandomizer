using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
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
        private Mock<IShiftRepository> _shiftRepositoryMock;

        public ShiftRepositoryTests()
        {
            _shiftRepositoryMock = new Mock<IShiftRepository>();
        }

        [TestMethod()]
        public async Task AddAsyncTest()
        {
            Shift shift = new Shift { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(8), ShiftType = EShiftType.Afternoon };
            _shiftRepositoryMock.Setup(x => x.AddAsync(shift));
            await _shiftRepositoryMock.Object.AddAsync(shift);
            _shiftRepositoryMock.Verify(x => x.AddAsync(shift), Times.Once()); 

        }

        [TestMethod()]
        public async Task AddListAsyncTest()
        {
            IEnumerable<Shift> shiftsBefore = new List<Shift>();
            _shiftRepositoryMock.Setup(mr => mr.ListAsync()).Returns(Task.FromResult(shiftsBefore));
            List<Shift> result = (await _shiftRepositoryMock.Object.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 0);
            //Insert shifts list
            IEnumerable<Shift> shifts = new List<Shift>
                {
                    new Shift { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(8), ShiftType = EShiftType.Afternoon },
                    new Shift { Id = 2, Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).AddHours(8), ShiftType = EShiftType.Afternoon },
                    new Shift { Id = 3, Start = DateTime.Now.AddDays(2), End = DateTime.Now.AddDays(2).AddHours(8), ShiftType = EShiftType.Afternoon },
                };
            _shiftRepositoryMock.Setup(mr => mr.AddListAsync(shifts));
            await _shiftRepositoryMock.Object.AddListAsync(shifts);
            _shiftRepositoryMock.Verify(x => x.AddListAsync(shifts), Times.Once());

            //Verify that list is now of 3 shifts
            _shiftRepositoryMock.Setup(mr => mr.ListAsync()).Returns(Task.FromResult(shifts));
            result = (await _shiftRepositoryMock.Object.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod()]
        public async Task ListAsyncTest()
        {
            IEnumerable<Shift> shifts = new List<Shift>
                {
                    new Shift { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddHours(8), ShiftType = EShiftType.Afternoon },
                    new Shift { Id = 2, Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(1).AddHours(8), ShiftType = EShiftType.Afternoon },
                    new Shift { Id = 3, Start = DateTime.Now.AddDays(2), End = DateTime.Now.AddDays(2).AddHours(8), ShiftType = EShiftType.Afternoon },
                };
            _shiftRepositoryMock.Setup(mr => mr.ListAsync()).Returns(Task.FromResult(shifts));
            List<Shift> result = (await _shiftRepositoryMock.Object.ListAsync()).ToList();
            Assert.AreEqual(result.Count, 3);
        }
    }
}