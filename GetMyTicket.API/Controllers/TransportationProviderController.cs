using GetMyTicket.Common.DTOs;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [ApiController]
    [Route("tp")]
    public class TransportationProviderController : Controller
    {
      private readonly ITransportationProviderService transportationProviderService;

        public TransportationProviderController(
            ITransportationProviderService transportationProviderService)
        {
            this.transportationProviderService = transportationProviderService;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll() {

          return await transportationProviderService.GetAll();
        }

    }
}
