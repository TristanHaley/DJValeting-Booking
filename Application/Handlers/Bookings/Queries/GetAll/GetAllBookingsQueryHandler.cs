using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Bookings.Queries.GetAll
{
    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, BookingListViewModel>
    {
        private readonly IDjValetingContext _context;
        private readonly IMapper              _mapper;

        public GetAllBookingsQueryHandler(IDjValetingContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }
        
        public async Task<BookingListViewModel> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            return new BookingListViewModel
            {
                Bookings = await _context.Bookings
                                         .OrderBy(booking => booking.PreferredDate)
                                         .ThenBy(booking => booking.Customer.Surname)
                                         .ProjectTo<BookingLookupModel>(_mapper.ConfigurationProvider)
                                         .ToListAsync(cancellationToken)
            };
        }
    }
}