using System;
using System.Linq;
using BloemenwinkelAPI.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BloemenwinkelAPI.Tests.Integration.Utils
{
    // Used for integration testing, based on https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<BloemenwinkelDatabaseContext>));

                services.Remove(descriptor);

                services.AddDbContext<BloemenwinkelDatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<BloemenwinkelDatabaseContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();
            });
        }

        // You can think of Action<...> as a reference to a method that is being passed.
        public void ResetAndSeedDatabase(Action<BloemenwinkelDatabaseContext> contextFiller)
        {
            // Retrieve a service scope and a database-context instance.
            using var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var db = scopedServices.GetRequiredService<BloemenwinkelDatabaseContext>();
            // Clear the database
            db.Bouqet.RemoveRange(db.Bouqet.ToList());
            db.Store.RemoveRange(db.Store.ToList());
            db.Order.RemoveRange(db.Order.ToList());
            db.SaveChanges();

            // execute the method using retrieved database as parameter
            contextFiller(db);

            db.SaveChanges();
        }
    }
}