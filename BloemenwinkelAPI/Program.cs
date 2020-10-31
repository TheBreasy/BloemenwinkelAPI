using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BloemenwinkelAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            // Add some logging
            .ConfigureLogging(x =>
            {
                x.ClearProviders();
                x.AddConsole();
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Indicate further configuration can be found in the startup file. 
                    // If you use visual studio code, you can navigate to the definition (right click on Startup)
                    webBuilder.UseStartup<Startup>();
                });
    }
}
