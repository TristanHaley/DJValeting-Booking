using System.Collections.Generic;

namespace Application.Handlers.Bookings.Queries.GetAll
{
    public class BookingListViewModel
    {
        public IList<BookingLookupModel> Bookings { get; set; }
    }
}