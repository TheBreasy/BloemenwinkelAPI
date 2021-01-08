using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloemenwinkelAPI.Controllers;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Snapshooter.Xunit;
using Xunit;

namespace BloemenwinkelAPI.Tests.Unit
{
    public class StoresControllerTests : IDisposable
    {
        // Mocking, the concept: https://stackoverflow.com/questions/2665812/what-is-mocking
        // Mocking, the library: https://github.com/Moq/moq4/wiki/Quickstart
        private readonly Mock<ILogger<StoresController>> _loggerMock;
        private readonly Mock<IStoreRepository> _storeRepoMock;
        private readonly StoresController _storesController;

        public StoresControllerTests()
        {
            // In our tests we choose to ignore whatever logging is being done. We still need to mock it to avoid 
            // null reference exceptions; loose mocks just handle whatever you throw at them.
            _loggerMock = new Mock<ILogger<StoresController>>(MockBehavior.Loose);
            _storeRepoMock = new Mock<IStoreRepository>(MockBehavior.Strict);
            _storesController = new StoresController(_storeRepoMock.Object, _loggerMock.Object);
        }
        public void Dispose()
        {
            _loggerMock.VerifyAll();
            _storeRepoMock.VerifyAll();

            _loggerMock.Reset();
            _storeRepoMock.Reset();
        }


        [Fact]
        public async Task TestGetAllGarages()
        {
            var returnSet = new[]
            {
                new Store
                {
                    Bouqets = new List<Bouqet>(200),
                    Id = 1,
                    Name = "test store 1",
                    Address = "Straat 1",
                    Region = "Antwerpen"

                },
                new Store
                {
                    Bouqets = new List<Bouqet>(200),
                    Id = 1,
                    Name = "test store 2",
                    Address = "Straat 2",
                    Region = "Temse"
                },
                new Store
                {
                    Bouqets = new List<Bouqet>(200),
                    Id = 1,
                    Name = "test store 3",
                    Address = "Straat 3",
                    Region = "Lennik"
                },
            };
            // Arrange
            _storeRepoMock.Setup(x => x.GetAllStores()).Returns(Task.FromResult((IEnumerable<Store>)returnSet)).Verifiable();

            // Act
            var storeResponse = await _storesController.GetAllStores();

            // Assert
            storeResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(storeResponse);
        }


        [Fact]
        public async Task TestGetOneStoreHappyPath()
        {
            var store = new Store()
            {
                Id = 1,
                Name = "12",
                Address = "12",
                Region = "12"
            };
            _storeRepoMock.Setup(x => x.GetOneStoreById(1)).Returns(Task.FromResult(store)).Verifiable();
            var storeResponse = await _storesController.StoreById(1);
            storeResponse.Should().BeOfType<OkObjectResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public async Task TestGetOneStoreNotFound()
        {
            _storeRepoMock.Setup(x => x.GetOneStoreById(1)).Returns(Task.FromResult(null as Store)).Verifiable();
            var storeResponse = await _storesController.StoreById(1);
            storeResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public async Task TestInsertOneStore()
        {
            var store = new Store()
            {
                Id = 1,
                Name = "abc",
                Address = "def",
                Region = "ghi"
            };
            _storeRepoMock.Setup(x => x.Insert("abc")).Returns(Task.FromResult(store)).Verifiable();
            var storeResponse = await _storesController.CreateStore(new StoreUpsertInput()
            {
                Name = "abc",
                Address = "def",
                Region = "ghi"
            });
            storeResponse.Should().BeOfType<CreatedResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public async Task TestUpdateOneStoreHappyPath()
        {
            var store = new Store()
            {
                Id = 1,
                Name = "jkl",
                Address = "mno",
                Region = "pqr"
            };
            _storeRepoMock.Setup(x => x.Update(1, "jkl", "mno", "pqr")).Returns(Task.FromResult(store)).Verifiable();
            var storeResponse = await _storesController.UpdateStore(1, new StoreUpsertInput()
            {
                Name = "jkl",
                Address = "mno",
                Region = "pqr"
            });
            storeResponse.Should().BeOfType<AcceptedResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public async Task TestUpdateOneStoreNotFound()
        {

            _storeRepoMock
                .Setup(x => x.Update(1, "jkl", "mno", "pqr"))
                .Throws<NotFoundException>()
                .Verifiable();
            var storeResponse = await _storesController.UpdateStore(1, new StoreUpsertInput()
            {
                Name = "jkl",
                Address = "mno",
                Region = "pqr"
            });
            storeResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(storeResponse);
        }
    }
}
