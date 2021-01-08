using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BloemenwinkelAPI.Database;
using BloemenwinkelAPI.Model;
using BloemenwinkelAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BloemenwinkelAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<BloemenwinkelDatabaseSettings>(
                Configuration.GetSection(nameof(BloemenwinkelDatabaseSettings)));

            services.AddSingleton<IBloemenwinkelDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BloemenwinkelDatabaseSettings>>().Value);

            services.AddSingleton<Orderservice>();

            services.AddControllers();

            services.AddDbContextPool<BloemenwinkelDatabaseContext>(
               dbContextOptions => dbContextOptions
                   .UseMySql(
                       // Replace with your connection string. Should be in your env but for example purposes this is _good enough_ for now
                       "server=localhost;user=root;password=root;database=bloemenwinkel",
                       // Replace with your server version and type.
                       mySqlOptions => mySqlOptions
                           .ServerVersion(new Version(5, 7, 24), ServerType.MySql)
                           .CharSetBehavior(CharSetBehavior.NeverAppend))
                   .UseLoggerFactory(
                       LoggerFactory.Create(
                           logging => logging
                               .AddConsole()
                               .AddFilter(level => level >= LogLevel.Information)))
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors());

            // Generates a swagger file automatically (https://swagger.io/) using swashbuckle (https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Flower store API",
                });
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, "BloemenwinkelAPI.xml");
                c.IncludeXmlComments(xmlPath);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BloemenwinkelDatabaseContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add a UI for swaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My flower store API V1");
            });
        }
    }
}
