using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Bogus;
using Bogus.Extensions;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeding
{
    public class CustomerSeeder : ContextSeeder
    {
        public override async Task<bool> SeedDatabase(IDjValetingContext context)
        {
            await using var transaction = await context.BeginTransactionAsync();

            var customers = FakerManager.CustomerFaker
                                        .RuleFor(customer => customer.Bookings,
                                                 _ => FakerManager.BookingFaker
                                                                  .RuleFor(booking => booking.VehicleType, faker => faker.PickRandom(context.VehicleTypes.ToList()))
                                                                  .GenerateBetween(1, 2))
                                        .GenerateBetween(4, 8);

            try
            {
                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to seed customers");
                await transaction.RollbackAsync();
                return false;
            }
        }

        public CustomerSeeder(ILogger<ContextSeeder> logger) : base(logger) { }
    }
}