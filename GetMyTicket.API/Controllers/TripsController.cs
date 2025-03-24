using GetMyTicket.Common.DTOs.Trip;
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

        [HttpGet]
        public async Task<IActionResult> Get(Guid tripId)
        { 
         var trip = await tripService.GetTrip(tripId);

            if (trip == null)
            {
                return NotFound();
            }

            //TODO -> RETURN DTO 
            else return Ok(trip);
        }


        [HttpGet("search")]
        //CURRENTLY, THE APP IGNORES TIMES ZONES TO SIMPLIFY THE DEVELOPMENT AT THIS STAGE OF DEVELOPMENT. THIS IS HOWEVER PLANNED IN THE NEAR FUTURE
        public async Task<IActionResult> GetAllTrips([FromQuery]SearchTripsDTO searchTripsDTO)
        {
            var trips = await tripService.GetAllSearchResultTrips(searchTripsDTO);

            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripDTO createTripDTO)
        {
            var tripId = await tripService.CreateTrip(createTripDTO);

            return Ok(tripId);

            //TODO -> replace with CreatedAtAction once we have a GET method
        }
    }
}
