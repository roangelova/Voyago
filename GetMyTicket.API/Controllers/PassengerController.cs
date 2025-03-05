using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService passengerService;

        public PassengerController( IPassengerService passengerService)
        {
            this.passengerService = passengerService; 
        }

        [HttpGet("by-user/{id}")]
        public Guid GetPassengerId(Guid id)
        {
            //TODO - IMPLEMENT
            return Guid.NewGuid();
        }

        [HttpPost]
        public IActionResult CreatePassenger(CreateOrEditPassengerDTO data)
        {
            //TODO - IMPLEMENT
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditPassenger(Guid id, CreateOrEditPassengerDTO data)
        {
            //TODO - IMPLEMENT
            return Ok();    
        }
    }
}
