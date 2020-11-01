using Moq;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RotaRandomizerTests.RepositoriesTests
{
    public class RotaRepositoryTest
    {
        private readonly RotaRepository _rotaRepository;

        public RotaRepositoryTest()
        {
            var dbContextMock = new Mock<RotaDbContext>();
            //TODO
        }

        [Fact]
        public void ListAsyncTest()
        {
            //TODO
        }
        [Fact]
        public void AddAsync()
        {
            //TODO
        }
        [Fact]
        public void Find()
        {
            //TODO

        }

    }
}
