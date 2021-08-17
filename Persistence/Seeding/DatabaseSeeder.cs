using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeding
{
    public class DatabaseSeeder : ContextSeeder
    {
        public override async Task<bool> SeedDatabase(IDjValetingContext context)
        {
            Logger.LogDebug("Beginning database seed");
            
            // Guard: Database already seeded
            if (context.Bookings.Any())
            {
                Logger.LogDebug("Database already seeded - skipping");
                return true;
            }

            try
            {
                // Ideally, failure to seed from a sub-seeder would be percolated up from here, rather than ignoring returns
                var vehicleTypeSeeder = new VehicleTypeSeeder(Logger);
                await vehicleTypeSeeder.SeedDatabase(context);

                var customerSeeder = new CustomerSeeder(Logger);
                await customerSeeder.SeedDatabase(context);

                var administratorSeeder = new AdministratorSeeder(Logger);
                await administratorSeeder.SeedDatabase(context);

                Logger.LogDebug("Seed complete");
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Database failed to seed");
                return false;
            }
        }

        public DatabaseSeeder(ILogger<ContextSeeder> logger) : base(logger) { }
    }
}