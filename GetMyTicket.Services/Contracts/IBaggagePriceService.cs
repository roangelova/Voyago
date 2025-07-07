using GetMyTicket.Common.DTOs.BaggagePrice;

namespace GetMyTicket.Service.Contracts
{
    public interface IBaggagePriceService
    {
        public Task<List<BaggagePriceDTO>> GetBaggagePricesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken);

        public Task CreateBaggagePricesForTransportationrovider(CreateBaggagePricesDTO createBaggagePricesDTO, CancellationToken cancellationToken);
    }
}
