using System;
using System.Threading.Tasks;
using DjValetingApi;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Presentation.Components.BookingForm
{
    public class BookingFormBase : ComponentBase
    {
        public BookingFormBase()
        {
            BookingCommand = new CreateBookingCommand
            {
                VehicleType = 1
            };
        }

        protected CreateBookingCommand BookingCommand { get; set; }
        
        protected                      DateTime? FormDateTime
        {
            get => BookingCommand.PreferredDate?.Date ?? ServerDateTime.UtcNow;
            set
            {
                if (BookingCommand.PreferredDate != value) BookingCommand.PreferredDate = value;
            }
        }
        
        [Inject] private IServerDateTime          ServerDateTime { get; set; } 
        [Inject] private ILogger<BookingFormBase> Logger         { get; set; }
        [Inject] private IBookingClient           BookingClient  { get; set; }

        protected string ValidateName(string arg)
        {
            return string.IsNullOrWhiteSpace(arg)
                ? "Must not be blank"
                : null;
        }

        protected async Task SubmitBookingAsync()
        {
            try
            {
                var result = await BookingClient.CreateAsync(BookingCommand);
                Logger.LogDebug("Booking create finished with code: {StatusCode}", result.StatusCode);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to submit booking");
            }
        }

        protected string ValidateDateIsInFuture(DateTime arg)
        {
            if (arg.Date < ServerDateTime.UtcNow.Date) return "Date cannot have passed!";
            if (arg.Date.Date == DateTime.UtcNow.Date) return "Date must be in the future!";
            return null;
        }
    }
}