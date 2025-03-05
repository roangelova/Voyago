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

        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO bookTripDTO)
        {
            var bookingId = await bookingService.CreateBooking(bookTripDTO);

            return Ok(bookingId);
        }



    }
}
