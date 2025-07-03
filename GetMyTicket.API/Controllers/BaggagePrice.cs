using GetMyTicket.Common.DTOs.BaggagePrice;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggagePrice : ControllerBase
    {
        private readonly IBaggagePriceService baggagePriceService;

        public BaggagePrice(IBaggagePriceService baggagePriceService)
        {
            this.baggagePriceService = baggagePriceService;
        }


        [HttpGet("{transportationProviderId}")]
        public async Task<List<BaggagePriceDTO>> GetBaggagePricesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken)
        {
            return await baggagePriceService.GetBaggagericesForTransportationProvider(transportationProviderId, cancellationToken);
        }
    }
}
