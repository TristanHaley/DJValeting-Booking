using System.Threading.Tasks;
using Application.Handlers.Bookings.Commands;
using Application.Handlers.Bookings.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BookingController : ControllerBase
    {
        /// <summary> POST: Creates a Booking. </summary>
        /// <param name="bookingCommand"> The Booking create command. </param>
        /// <returns> </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateBookingCommand bookingCommand)
        {
            await Mediator.Send(bookingCommand).ConfigureAwait(false);
            return NoContent();
        }

        /// <summary> GET: Gets all the bookings. </summary>
        /// <returns> Ok </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BookingListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllBookingsQuery())
                                   .ConfigureAwait(false));
        }
    }
}