using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    //AUTHORIZE set for testing purposes;

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

        [HttpPost]
        public async Task<IActionResult> CreateTransportationProvider(CreateTransportationProviderDTO data)
        {

            var entity = await transportationProviderService.Add(data);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = entity.TransportationProviderId.ToString() },
                new { entity.Name, entity.Email, entity.Description, entity.Address });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById( Guid id)
        {
            var entity = await transportationProviderService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);

        }
    }
}
