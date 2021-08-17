using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeding
{
    public class VehicleTypeSeeder : ContextSeeder
    {
        public override async Task<bool> SeedDatabase(IDjValetingContext context)
        {
            await using var transaction = await context.BeginTransactionAsync();
            
            var vehicleTypes = new List<VehicleType>
            {
                new() { Description = "Small",  Price = 6 },
                new() { Description = "Medium", Price = 8 },
                new() { Description = "Large",  Price = 12 },
                new() { Description = "Van",    Price = 15 }
            };

            try
            {
                await context.VehicleTypes.AddRangeAsync(vehicleTypes);
                await context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to seed vehicle types");
                await transaction.RollbackAsync();
                return false;
            }
        }

        public VehicleTypeSeeder(ILogger<ContextSeeder> logger) : base(logger) { }
    }
}