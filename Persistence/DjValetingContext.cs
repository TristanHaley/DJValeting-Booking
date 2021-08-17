using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence
{
    public class DjValetingContext : DbContext, IDjValetingContext
    {
        public DjValetingContext(DbContextOptions<DjValetingContext> options) : base(options){}

        public Task<bool>                 EnsureCreatedAsync()
        {
            return Database.EnsureCreatedAsync();
        }

        public DbSet<Booking>       Bookings        { get; set; }
        public DbSet<Administrator> Administrators  { get; set; }
        public DbSet<Customer>      Customers       { get; set; }
        public DbSet<VehicleType>   VehicleTypes    { get; set; }

        
        public Task<IEnumerable<string>> GetPendingMigrationsAsync()
        {
            return Database.GetPendingMigrationsAsync();
        }
        
        public Task MigrateAsync()
        {
            return Database.MigrateAsync();
        }
        
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return Database.BeginTransactionAsync();
        }
    }
}