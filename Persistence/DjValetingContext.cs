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
        
        public DbSet<Booking>        Bookings           { get; set; }
        public DbSet<Administrator>        Administrators     { get; set; }
        public DbSet<Customer>        Customers          { get; set; }
        public DbSet<VehicleType>        VehicleTypes       { get; set; }
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return Database.BeginTransactionAsync();
        }
    }
}