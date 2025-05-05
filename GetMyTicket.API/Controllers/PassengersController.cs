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
        public async Task<GetPassengerDTO> GetPassenger(Guid userId)
        {
            var passenger =  await passengerService.GetPassengerAsync(userId);
            return passenger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePassenger(CreateOrEditPassengerDTO createOrEditPassengerDTO)
        {
            Guid passengerId = await passengerService.CreatePassenger(createOrEditPassengerDTO);

            return Ok(passengerId) ;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPassenger(Guid id, CreateOrEditPassengerDTO data)
        {
            var updatedPassenger = await passengerService.EditPassenger(id, data);

            if (updatedPassenger == null)
                return NotFound();

            return Ok(updatedPassenger);
        }
    }
}
