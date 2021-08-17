using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.Bookings.Commands
{
    public class BookingCreated : INotification
    {
        #region Constructors

        public BookingCreated(string   customerFirstName,
                              string   customerSurname,
                              DateTime preferredDate,
                              string   vehicleType)
        {
            CustomerFirstName     = customerFirstName;
            CustomerSurname       = customerSurname;
            PreferredDate         = preferredDate;
            VehicleType           = vehicleType;
        }

        #endregion

        public string   CustomerFirstName { get; }
        public string   CustomerSurname   { get; }
        public DateTime PreferredDate     { get; }
        public string   VehicleType       { get; }
    }
    
    public class BookingCreatedHandler : INotificationHandler<BookingCreated>
    {
        private readonly ILogger<BookingCreated> _logger;
        public BookingCreatedHandler(ILogger<BookingCreated> logger)
        {
            _logger = logger;
        }

        public Task Handle(BookingCreated notification, CancellationToken cancellationToken)
        {
            // TODO: This would contain the IService(s) to notify the customer / staff of the booking
            _logger.LogInformation("Created \"{@notification}\"", notification);
            return Task.CompletedTask;
        }
    }
}