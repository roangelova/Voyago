using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [ApiController]
    [Route("api/transportationproviders")]
    public class TransportationProvidersController : ControllerBase
    {
        private readonly ITransportationProviderService transportationProviderService;

        public TransportationProvidersController(
            ITransportationProviderService transportationProviderService)
        {
            this.transportationProviderService = transportationProviderService;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll()
        {
            return await transportationProviderService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await transportationProviderService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);

        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTransportationProvider([FromForm] CreateTransportationProviderDTO data)
        {
            var entity = await transportationProviderService.Add(data);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = entity.TransportationProviderId.ToString() },
                new { entity.Name, entity.Email, entity.Description, entity.Address });
        }

        [HttpPut("{id}")]
       public async Task<IActionResult> PutTransportationProvider(Guid id,[FromForm] EditTransportationProvider data)
        {

            var updatedEntity = await transportationProviderService.Update(id, data);

            return Ok(updatedEntity);
        }
    }
}
