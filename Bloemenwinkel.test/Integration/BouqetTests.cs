using System.Threading.Tasks;
using BloemenwinkelAPI.Model.Domain;
using BloemenwinkelAPI.Model.Web;
using BloemenwinkelAPI.Tests.Integration.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Snapshooter;
using Snapshooter.Xunit;
using Xunit;

namespace BloemenwinkelAPI.Tests.Integration
{
    public class BouqetTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BouqetTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetBouquetsEndPointReturnsNoDataWhenDbIsEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("/bouqet");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetBouquetsEndPointReturnsSomeDataWhenDbIsNotEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Bouqet.Add(new Bouqet() { Id = 1, StoreId = 1, Name = "Tulip", Price = 9.99, Description = "a beautiful red tulip" });
                db.Bouqet.Add(new Bouqet() { Id = 2, StoreId = 2, Name = "Rose", Price = 9.99, Description = "a beautiful red rose" });
            });
            var response = await client.GetAsync("/bouqet");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetBouqetsById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("/bouqet/1");
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBouqetByIdReturnBouqetsIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Bouqet.Add(new Bouqet() { Id = 1, StoreId = 1, Name = "Tulip", Price = 9.99, Description = "a beautiful red tulip" });
            });
            var response = await client.GetAsync("/bouqet/1");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task DeleteBouqetByIdReturns404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Bouqet.Add(new Bouqet() { Id = 1, StoreId = 1, Name = "Tulip", Price = 9.99, Description = "a beautiful red tulip" });
            });
            var response = await client.DeleteAsync("/bouqet/2");
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DeleteBouqetByIdReturnsDeletesIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Bouqet.Add(new Bouqet() { Id = 1, StoreId = 1, Name = "Tulip", Price = 9.99, Description = "a beautiful red tulip" });
            });
            var beforeDeleteResponse = await client.GetAsync("/bouqet/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("/bouqet/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("/bouqet/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task InsertBouqetReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new BouqetUpsertInput
                {
                    Name = "Tulip"
                }
            };
            var createResponse = await client.PostAsync("/bouqet", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<BouqetWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.Name.Should().Be("Tulip");
            var getResponse = await client.GetAsync($"/bouqet/{body.Id}");
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InsertBouqetThrowsErrorOnEmptyName()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new BouqetUpsertInput
                {
                    Name = string.Empty
                }
            };
            var createResponse = await client.PostAsync("/bouqet", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task InsertBouqetThrowsErrorOnGiganticName()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new BouqetUpsertInput
                {
                    Name = new string('c', 10001)
                }
            };
            var createResponse = await client.PostAsync("/bouqet", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateBouqetReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new BouqetUpsertInput
                {
                    Name = "Tulip"
                }
            };
            var patchResponse = await client.PatchAsync("/bouqet/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateBouqetReturnsAnUpdatedResult()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Bouqet.Add(new Bouqet() { Id = 1, StoreId = 1, Name = "Tulip", Price = 9.99, Description = "a beautiful red tulip" });
            });
            var request = new
            {
                Body = new BouqetUpsertInput
                {
                    Name = "Tulip"
                }
            };
            var patchResponse = await client.PatchAsync("/bouqet/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<BouqetWebOutput>(await patchResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.Name.Should().Be("Tulip");
            var getResponse = await client.GetAsync($"/bouqet/{body.Id}");
            getResponse.EnsureSuccessStatusCode();
            Snapshot.Match(getResponse.Content.ReadAsStringAsync(), new SnapshotNameExtension("_Content"));
            Snapshot.Match(getResponse, new SnapshotNameExtension("_Full"));
        }
    }
}