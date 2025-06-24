using System.Net;
using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/passengers")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IPassengerService passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            this.passengerService = passengerService;
        }


        [HttpGet("by-user/{userId}")]
        public async Task<List<GetPassengerDTO>> GetPassenger(Guid userId, CancellationToken cancellationToken)
        {
            var passengers =  await passengerService.GetPassengerAsync(userId, cancellationToken);
            return passengers;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePassenger(CreatePassengerDTO CreatePassengerDTO)
        {
            Guid passengerId = await passengerService.CreatePassenger(CreatePassengerDTO);

            return Ok(passengerId) ;
        }

        [HttpPut]
        public async Task<IActionResult> EditPassenger(EditPassengerDTO data, CancellationToken cancellationToken)
        {
            var updatedPassenger = await passengerService.EditPassenger(data, cancellationToken);

            if (updatedPassenger == null)
                return NotFound();

            return Ok(updatedPassenger);
        }

        [HttpGet("{bookingId}")]
        public async Task<List<GetNameAndAgePassengerDataDTO>> GetPassengersForBooking(Guid bookingId, CancellationToken cancellationToken = default)
        {
            return await passengerService.GetPassengersForBooking(bookingId, cancellationToken);
        }
    }
}
