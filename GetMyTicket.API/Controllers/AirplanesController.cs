﻿using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/airplanes")]
    [ApiController]
    public class AirplanesController : ControllerBase
    {
        private readonly IAirplaneService airplaneService;

        public AirplanesController(IAirplaneService airplaneService)
        {
            this.airplaneService = airplaneService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirplane (CreateAirplaneDTO data)
        {

            var entity = await airplaneService.Add(data);

            string manufacturer = Enum.GetName(entity.AirplaneManufacturer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = entity.VehicleId },
                new { manufacturer, entity.Model });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (Guid id)
        {
            var entity = await airplaneService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
