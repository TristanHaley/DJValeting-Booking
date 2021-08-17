using System;
using MediatR;

namespace Application.Handlers.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<bool>
    {
        public string   CustomerFirstName     { get; set; }
        public string   CustomerSurname       { get; set; }
        public string   CustomerContactNumber { get; set; }
        public string   CustomerEmail         { get; set; }
        public int      DateLeeway            { get; set; }
        public DateTime PreferredDate         { get; set; }
        public int      VehicleType           { get; set; }
    }
}