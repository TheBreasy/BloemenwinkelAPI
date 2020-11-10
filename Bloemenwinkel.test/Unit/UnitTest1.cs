using System;
using System.Collections.Generic;
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
    public class StoreControllerTests : IDisposable
    {

        // Mocking, the concept: https://stackoverflow.com/questions/2665812/what-is-mocking
        // Mocking, the library: https://github.com/Moq/moq4/wiki/Quickstart
        private readonly Mock<ILogger<StoresController>> _loggerMock;
        private readonly Mock<IStoreRepository> _storeRepoMock;
        private readonly StoresController _storeController;

        public StoreControllerTests()
        {
            // In our tests we choose to ignore whatever logging is being done. We still need to mock it to avoid 
            // null reference exceptions; loose mocks just handle whatever you throw at them.
            _loggerMock = new Mock<ILogger<StoresController>>(MockBehavior.Loose);
            _storeRepoMock = new Mock<IStoreRepository>(MockBehavior.Strict);
            _storeController = new StoresController(_storeRepoMock.Object, _loggerMock.Object);
        }
        public void Dispose()
        {
            _loggerMock.VerifyAll();
            _storeRepoMock.VerifyAll();

            _loggerMock.Reset();
            _storeRepoMock.Reset();
        }


        [Fact]
        public void TestGetAllStores()
        {
            var returnSet = new[]
            {
                new Store
                {
                    Name = "test store 1",
                    Bouqets = new List<Bouqet>(200),
                    Address = "AntwerpseStraat 138",
                    Region = "Antwerpen",
                    Id = 1
                },
                new Store
                {
                    Name = "test store 2",
                    Bouqets = new List<Bouqet>(200),
                    Address = "MechelseStraat 138",
                    Region = "Mechelen",
                    Id = 2
                },
                new Store
                {
                    Name = "test store 3",
                    Bouqets = new List<Bouqet>(200),
                    Address = "LeuvenseStraat 138",
                    Region = "Leuven",
                    Id = 3
                },
            };
            // Arrange
            _storeRepoMock.Setup(x => x.GetAllStores()).Returns(returnSet).Verifiable();

            // Act
            var storeResponse = _storeController.GetAllStores();

            // Assert
            storeResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(storeResponse);
        }


        [Fact]
        public void TestGetOneStoreHappyPath()
        {
            var store = new Store()
            {
                Id = 1,
                Name = "12"
            };
            _storeRepoMock.Setup(x => x.GetOneStoreById(1)).Returns(store).Verifiable();
            var storeResponse = _storeController.StoreById(1);
            storeResponse.Should().BeOfType<OkObjectResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public void TestGetOneStoreNotFound()
        {
            _storeRepoMock.Setup(x => x.GetOneStoreById(1)).Returns(null as Store).Verifiable();
            var storeResponse = _storeController.StoreById(1);
            storeResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public void TestInsertOneStore()
        {
            var store = new Store()
            {
                Id = 1,
                Name = "abcdef"
            };
            _storeRepoMock.Setup(x => x.Insert("abcdef")).Returns(store).Verifiable();
            var storeResponse = _storeController.CreateStore(new StoreUpsertInput()
            {
                Name = "abcdef"
            });
            storeResponse.Should().BeOfType<CreatedResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public void TestUpdateOneStoreHappyPath()
        {
            var store = new Store()
            {
                Id = 1,
                Name = "ghijkl"
            };
            _storeRepoMock.Setup(x => x.Update(1, "ghijkl")).Returns(store).Verifiable();
            var storeResponse = _storeController.UpdateStore(1, new StoreUpsertInput()
            {
                Name = "ghijkl"
            });
            storeResponse.Should().BeOfType<AcceptedResult>();
            Snapshot.Match(storeResponse);
        }

        [Fact]
        public void TestUpdateOneStoreNotFound()
        {

            _storeRepoMock
                .Setup(x => x.Update(1, "ghijkl"))
                .Throws<NotFoundException>()
                .Verifiable();
            var storeResponse = _storeController.UpdateStore(1, new StoreUpsertInput()
            {
                Name = "ghijkl"
            });
            storeResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(storeResponse);
        }
    }
}
