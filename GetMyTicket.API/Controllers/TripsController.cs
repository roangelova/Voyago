using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllTrips([FromQuery]SearchTripsDTO searchTripsDTO)
        {
            var trips = await tripService.GetAllSearchResultTrips(searchTripsDTO);

            return Ok(trips);
        }
    }
}
