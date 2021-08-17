using Application.Interfaces.Mapping;
using AutoMapper;
using Domain.Models;

namespace Application.Handlers.Bookings.Queries.GetAll
{
    public class BookingLookupModel : IHaveCustomMapping
    {
        public string CustomerName     { get; set; }
        public string CustomerNumber   { get; set; }
        public string CustomerEmail    { get; set; }
        public string VehicleType      { get; set; }
        public string SelectedDateTime { get; set; }
        public int    Leeway           { get; set; }
        public bool   HasBeenNotified  { get; set; }
        
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Booking, BookingLookupModel>()
                         .ForMember(bookingDto => bookingDto.CustomerName, opt => opt.MapFrom(booking => $"{booking.Customer.Surname}, {booking.Customer.Firstname}"))
                         .ForMember(bookingDto => bookingDto.CustomerNumber, opt => opt.MapFrom(booking => booking.Customer.ContactNumber))
                         .ForMember(bookingDto => bookingDto.CustomerEmail, opt => opt.MapFrom(booking => booking.Customer.ContactEmail))
                         .ForMember(bookingDto => bookingDto.VehicleType, opt => opt.MapFrom(booking => booking.VehicleType.Description))
                         .ForMember(bookingDto => bookingDto.Leeway, opt => opt.MapFrom(booking => booking.DateLeeway))
                         .ForMember(bookingDto => bookingDto.HasBeenNotified, opt => opt.MapFrom(booking => booking.Notified))
                         .ForMember(bookingDto => bookingDto.SelectedDateTime, opt => opt.MapFrom(booking => booking.PreferredDate));
        }
    }
}