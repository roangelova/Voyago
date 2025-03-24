using GetMyTicket.Service.Contracts;
using GetMyTicket.Common.DTOs.Vehicle;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/trains")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService trainService;

        public TrainController(ITrainService trainService)
        {
            this.trainService = trainService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrain(AddTrainDTO data)
        {
            var entity = await trainService.Add(data);

            return CreatedAtAction(
                nameof(GetById),
                new { id = entity.TrainId }, 
                entity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await trainService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
