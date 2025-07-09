using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;
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

        [HttpGet("{tripId}")]
        public async Task<IActionResult> Get(Guid tripId, CancellationToken cancellationToken = default)
        {
            var trip = await tripService.GetTrip(tripId, cancellationToken);

            if (trip == null)
            {
                return NotFound();
            }

            //TODO -> RETURN DTO 
            else return Ok(trip);
        }


        [HttpGet("search")]
        //CURRENTLY, THE APP IGNORES TIMES ZONES TO SIMPLIFY THE DEVELOPMENT AT THIS STAGE OF DEVELOPMENT. THIS IS HOWEVER PLANNED IN THE NEAR FUTURE
        public async Task<IActionResult> GetAllTrips([FromQuery] SearchTripsDTO searchTripsDTO, CancellationToken cancellationToken)
        {
            var trips = await tripService.GetAllSearchResultTrips(searchTripsDTO, cancellationToken);

            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripDTO createTripDTO)
        {
            var tripId = await tripService.CreateTrip(createTripDTO);

            return CreatedAtAction(nameof(Get), new {tripId}, tripId);
        }

        [HttpPut]
        public async Task<Trip> UpdateTrip(UpdateTripDTO updateTripDTO)
        {
            return await tripService.UpdateTrip(updateTripDTO);
        }
    }
}
