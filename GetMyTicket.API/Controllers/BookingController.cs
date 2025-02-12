using GetMyTicket.Common.DTOs;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost(nameof(BookTrip))]
        public async Task<IActionResult> BookTrip(BookTripDTO bookTripDTO)
        {
            var bookingId = await bookingService.BookTrip(bookTripDTO);

            return Ok(bookingId);
        }



    }
}
