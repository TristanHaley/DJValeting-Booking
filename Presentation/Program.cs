using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Serilog;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope  = host.Services.CreateScope();
            var       logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = scope.ServiceProvider.GetService<DjValetingContext>();

                logger.LogDebug("Attempting database migration");

                await context.Database.MigrateAsync();
                logger.LogDebug("Migration complete");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to migrate the database");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                            webBuilder.UseStartup<Startup>();
                            webBuilder.UseIISIntegration();
                        })
                       .ConfigureAppConfiguration((hostingContext, configuration) =>
                        {
                            var hostEnvironment = hostingContext.HostingEnvironment;
                            configuration.SetBasePath(hostEnvironment.ContentRootPath);
                            configuration.AddJsonFile("appsettings.json", false, true)
                                         .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true);
                            configuration.AddEnvironmentVariables();
                        })
                       .UseSerilog((hostingContext, loggerBuilder) => loggerBuilder.ReadFrom.Configuration(hostingContext.Configuration));
        }
    }
}