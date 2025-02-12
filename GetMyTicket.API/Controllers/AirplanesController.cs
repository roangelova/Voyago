using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Service.Contracts;
using GetMyTicket.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirplanesController : ControllerBase
    {
        private readonly IAirplaneService airplaneService;

        public AirplanesController(IAirplaneService airplaneService)
        {
            this.airplaneService = airplaneService;
        }

        [HttpPost]
        public async Task<IActionResult> Add (AddAirplaneDTO data)
        {

            var entity = await airplaneService.Add(data);

            string manufacturer = Enum.GetName(entity.AirplaneManufacturer);

            return CreatedAtAction(
                nameof(GetById),
                new { Id = entity.VehicleId },
                new { manufacturer, entity.Model });
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById ([FromQuery] Guid Id)
        {
            var entity = await airplaneService.GetById(Id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
