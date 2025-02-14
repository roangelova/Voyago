using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    //AUTHORIZE set for testing purposes;

    [ApiController]
    [Route("api/transportations")]
    public class TransportationProvidersController : ControllerBase
    {
        private readonly ITransportationProviderService transportationProviderService;

        public TransportationProvidersController(
            ITransportationProviderService transportationProviderService)
        {
            this.transportationProviderService = transportationProviderService;
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll()
        {
            return await transportationProviderService.GetAll();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddTpDTO data)
        {

            var entity = await transportationProviderService.Add(data);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = entity.TransportationProviderId.ToString() },
                new { entity.Name, entity.Email, entity.Description, entity.Address });
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult> GetById([FromQuery] Guid Id)
        {
            var entity = await transportationProviderService.GetById(Id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);

        }
    }
}
