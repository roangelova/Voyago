using GetMyTicket.Common.DTOs.Booking;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : Controller
    {
        private readonly IBookingService bookingService;

        public BookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO bookTripDTO)
        {
            var bookingId = await bookingService.CreateBooking(bookTripDTO);

            return Ok(bookingId);
        }

       [HttpGet("{userId}")]
       public async Task<IEnumerable<ListBookingDTO>> GetUserBookings(Guid userId, CancellationToken cancellationToken)
       {
           return await bookingService.GetUserBookings(userId, cancellationToken);
       }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> CancelBooking (Guid bookingId)
        {
            var result = await bookingService.CancelBooking(bookingId);

           if(result == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}
