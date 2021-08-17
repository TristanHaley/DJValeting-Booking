using Application.Handlers.Bookings.Queries.GetAll;
using FluentValidation;

namespace Application.Handlers.Bookings.Commands
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.CustomerFirstName).NotEmpty();
            RuleFor(x => x.VehicleType).GreaterThan(0);
            RuleFor(x => x.CustomerContactNumber).NotEmpty();
            RuleFor(x => x.DateLeeway).GreaterThanOrEqualTo(0).LessThanOrEqualTo(3);
            RuleFor(x => x.CustomerSurname).NotEmpty();
            RuleFor(x => x.CustomerEmail).EmailAddress();
        }
    }
}