using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService tripService;

        public TripsController(ITripService tripService)
        {
            this.tripService = tripService;
        }

        [HttpPost(nameof(GetAllSearchResultTrips))]
        public async Task<IActionResult> GetAllSearchResultTrips(SearchTripsDTO searchTripsDTO)
        {
            var trips = await tripService.GetAllSearchResultTrips(searchTripsDTO);

            return Ok(trips);
        }
    }
}
