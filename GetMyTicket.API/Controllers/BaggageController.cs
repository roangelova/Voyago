using GetMyTicket.Common.DTOs.Baggage;
using GetMyTicket.Common.DTOs.BaggagePrice;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageController : ControllerBase
    {
        private readonly IBaggagePriceService baggagePriceService;
        private readonly IBaggageItemService baggageItemService;


        public BaggageController(
            IBaggagePriceService baggagePriceService, 
            IBaggageItemService baggageItemService)
        {
            this.baggagePriceService = baggagePriceService;
            this.baggageItemService = baggageItemService;
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<List<BaggageItemDTO>> GetBaggageItemsForBooking(Guid bookingId, CancellationToken cancellationToken)
        {
            return await baggageItemService.GetBaggageItemsForBooking(bookingId, cancellationToken);
        }

        [HttpGet("tp/{transportationProviderId}")]
        public async Task<List<BaggagePriceDTO>> GetBaggagePricesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken)
        {
            return await baggagePriceService.GetBaggagePricesForTransportationProvider(transportationProviderId, cancellationToken);
        }

        [HttpPost]
        public async Task CreateBaggagePrices(CreateBaggagePricesDTO createBaggagePricesDTO, CancellationToken cancellationToken)
        {
           await baggagePriceService.CreateBaggagePricesForTransportationrovider(createBaggagePricesDTO, cancellationToken);
        }
    }
}
