using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, bool>
    {
        private readonly ILogger<CreateBookingCommandHandler> _logger;
        private readonly IMediator                              _mediator;
        private readonly IDjValetingContext                     _context;
        public CreateBookingCommandHandler(ILogger<CreateBookingCommandHandler> logger, IMediator mediator, IDjValetingContext context)
        {
            _logger   = logger;
            _mediator = mediator;
            _context  = context;
        }

        public async Task<bool> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Handling \"{nameof(CreateBookingCommand)}\" request, via \"{nameof(CreateBookingCommandHandler)}\" handler");

            await using var contextTransaction = await _context.BeginTransactionAsync();

            try
            {
                var customer = await GetCustomerAsync(request.CustomerFirstName, request.CustomerSurname, request.CustomerEmail, request.CustomerContactNumber, cancellationToken);
                var booking = new Booking
                {
                    Customer   = customer,
                    Notified   = false,
                    Status     = BookingStatus.Pending,
                    DateLeeway = request.DateLeeway,
                    VehicleType = await _context.VehicleTypes.FindAsync(new object[]
                    {
                        request.VehicleType
                    }, cancellationToken),
                    PreferredDate = request.PreferredDate
                };

                await _context.Bookings.AddAsync(booking, cancellationToken);
                await _context.SaveChangesAsync(true, cancellationToken);
                await contextTransaction.CommitAsync(cancellationToken);
                await _mediator.Publish(new BookingCreated(booking.Customer.Firstname, booking.Customer.Surname, booking.PreferredDate, booking.VehicleType.Description), cancellationToken).ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to handle create request");
                _logger.LogDebug("Rolling back transaction");
                await contextTransaction.RollbackAsync(cancellationToken);
                _logger.LogDebug("Rollback complete");
                return false;
            }
        }

        private async Task<Customer> GetCustomerAsync(string firstName, string surname, string email, string number, CancellationToken cancellationToken)
        {
            try
            {
                // Use existing customer, if found
                var existingCustomer = await _context.Customers.Where(customer => customer.Firstname     == firstName &&
                                                                                  customer.Surname       == surname   &&
                                                                                  customer.ContactEmail  == email     &&
                                                                                  customer.ContactNumber == number)
                                                     .SingleOrDefaultAsync(cancellationToken);

                if (existingCustomer != null) return existingCustomer;

                var newCustomer = new Customer
                {
                    Firstname     = firstName,
                    Surname       = surname,
                    ContactEmail  = email,
                    ContactNumber = number
                };

                return newCustomer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving or creating a customer");
                throw;
            }
        }
    }
}