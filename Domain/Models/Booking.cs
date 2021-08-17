using System;
using Domain.Models.Enums;

namespace Domain.Models
{
    public class Booking
    {
        public         DateTime      PreferredDate { get; set; }
        public         int           DateLeeway    { get; set; }
        public virtual VehicleType   VehicleType   { get; set; }
        public virtual Customer      Customer      { get; set; }
        public         bool          Notified      { get; set; }
        public         BookingStatus Status        { get; set; }
    }
}