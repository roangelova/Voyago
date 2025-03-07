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

        /// <summary>
        /// Returns the passenger Id, associated with the user with the provided id
        /// </summary>
        /// <param name="id">The user Id to search by</param>
        /// <returns></returns>
        [HttpGet("by-user/{id}")]
        public async Task<Guid> GetPassengerId(Guid id)
        {
            return await passengerService.GetPassengerIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePassenger(CreateOrEditPassengerDTO createOrEditPassengerDTO)
        {
            Guid passengerId = await passengerService.CreatePassenger(createOrEditPassengerDTO);

            return CreatedAtAction(nameof(GetPassengerId), new { id = passengerId }, passengerId); ;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPassenger(Guid id, CreateOrEditPassengerDTO data)
        {
            var updatedPassenger = await passengerService.EditPassenger(id, data);

            if (updatedPassenger == null)
                return BadRequest();

            return Ok(updatedPassenger);
        }
    }
}
