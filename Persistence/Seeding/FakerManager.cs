using Bogus;
using Domain.Models;
using Domain.Models.Enums;

namespace Persistence.Seeding
{
    public static class FakerManager
    {
        public static Faker<Customer> CustomerFaker =>
            new Faker<Customer>()
               .RuleFor(customer => customer.Firstname, faker => faker.Person.FirstName)
               .RuleFor(customer => customer.Surname, faker=>faker.Person.LastName)
               .RuleFor(customer => customer.ContactEmail, faker=>faker.Person.Email)
               .RuleFor(customer => customer.ContactNumber, faker => faker.Person.Phone);

        public static Faker<Booking> BookingFaker =>
            new Faker<Booking>()
               .RuleFor(booking => booking.Notified, false)
               .RuleFor(booking => booking.Status, faker => faker.PickRandom<BookingStatus>())
               .RuleFor(booking => booking.DateLeeway, faker => faker.Random.Int(0, 3))
               .RuleFor(booking => booking.PreferredDate, faker => faker.Date.Soon());
    }
}