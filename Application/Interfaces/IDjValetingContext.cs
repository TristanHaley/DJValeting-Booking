using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces
{
    public interface IDjValetingContext
    {
        int                         SaveChanges();
        int                         SaveChanges(bool acceptAllChangesOnSuccess);
        public Task<bool>           EnsureCreatedAsync();
        public DbSet<Booking>       Bookings       { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Customer>      Customers      { get; set; }
        public DbSet<VehicleType>   VehicleTypes   { get; set; }

        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task                        MigrateAsync();
        public Task<IEnumerable<string>>   GetPendingMigrationsAsync();
        
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool              acceptAllChangesOnSuccess, CancellationToken cancellationToken);
    }
}