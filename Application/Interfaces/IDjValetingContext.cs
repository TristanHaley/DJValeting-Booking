﻿using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces
{
    public interface IDjValetingContext
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Booking> Administrators { get; set; }
        public DbSet<Booking> Customers { get; set; }
        public DbSet<Booking> VehicleTypes { get; set; }

        public abstract Task<IDbContextTransaction> BeginTransactionAsync();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool              acceptAllChangesOnSuccess, CancellationToken cancellationToken);
    }
}