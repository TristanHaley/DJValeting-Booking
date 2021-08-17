using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeding
{
    public class AdministratorSeeder : ContextSeeder
    {
        public AdministratorSeeder(ILogger<ContextSeeder>          logger) : base(logger) { }
        public override async Task<bool> SeedDatabase(IDjValetingContext context)
        {
            await using var transaction = await context.BeginTransactionAsync();
            
            var administrators = new List<Administrator>
            {
                new() { Email = "Boss@DjValeting.com", Password = "BadPassword123_" },
                new() { Email = "Clerk@DjValeting.com", Password = "BadPassword123_" }
            };

            try
            {
                await context.Administrators.AddRangeAsync(administrators);
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
    }
}