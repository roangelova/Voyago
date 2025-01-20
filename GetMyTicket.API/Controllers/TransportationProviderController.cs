using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [ApiController]
    [Route("tp")]
    public class TransportationProviderController : ControllerBase
    {
        private readonly ITransportationProviderService transportationProviderService;

        public TransportationProviderController(
            ITransportationProviderService transportationProviderService)
        {
            this.transportationProviderService = transportationProviderService;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll()
        {

            return await transportationProviderService.GetAll();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AddTpDTO data)
        {

           var entity = await transportationProviderService.Add(data);

            return CreatedAtAction(nameof(Add), new { entity.Name, entity.Email, entity.Description, entity.Address});
        }
    }
}
