using System;
using System.Threading.Tasks;
using DjValetingApi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Presentation.Pages.Admin
{
    public class AdminBase : ComponentBase
    {
        [Inject] private ILogger<AdminBase> Logger        { get; set; }
        [Inject] private IBookingClient     BookingClient { get; set; }
        protected override async Task               OnInitializedAsync()
        {
            try
            {
                var request = await BookingClient.GetAllAsync();
                BookingList = request.Result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to fetch booking list");
            }
        }

        protected BookingListViewModel BookingList { get; set; }
    }
}