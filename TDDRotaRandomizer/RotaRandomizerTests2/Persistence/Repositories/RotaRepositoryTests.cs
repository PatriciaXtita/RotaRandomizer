using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Models;
using RotaRandomizer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RotaRandomizer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class RotaRepositoryTests
    {
        private Mock<IRotaRepository> _rotaRepositoryMock;

        [TestMethod()]
        public void ListAsyncTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async Task AddAsyncTest()
        {
            Rota rota = new Rota { Id = 1, Start = DateTime.Now, End = DateTime.Now.AddDays(14) };
            _rotaRepositoryMock.Setup(x => x.AddAsync(rota));
            await _rotaRepositoryMock.Object.AddAsync(rota);
            _rotaRepositoryMock.Verify(x => x.AddAsync(rota), Times.Once()); 
        }

        [TestMethod()]
        public void FindTest()
        {
            Assert.Fail();
        }
    }
}