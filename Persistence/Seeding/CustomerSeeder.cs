using System.Threading.Tasks;
using Application.Interfaces;
using Bogus;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Persistence.Seeding
{
    public class CustomerSeeder : ContextSeeder
    {
        private Faker<Customer> CustomerFaker =>
            new Faker<Customer>()
               .Ignore(customer => customer.Bookings)
               .RuleFor(customer => customer.Firstname, faker => faker.Person.FirstName)
               .RuleFor(customer => customer.Surname, faker=>faker.Person.LastName)
               .RuleFor(customer => customer.ContactEmail, faker=>faker.Person.Email)
               .RuleFor(customer => customer.ContactNumber, faker => faker.Person.Phone);
        
        public override async Task<bool> SeedDatabase(IDjValetingContext context)
        {
            // Warn: Work paused
            using var transaction = await context.BeginTransactionAsync();

            return false;
        }
    }
}