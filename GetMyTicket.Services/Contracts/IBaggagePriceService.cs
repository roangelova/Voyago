using GetMyTicket.Common.DTOs.BaggagePrice;

namespace GetMyTicket.Service.Contracts
{
    public interface IBaggagePriceService
    {
        public Task<List<BaggagePriceDTO>> GetBaggagericesForTransportationProvider(Guid transportationProviderId, CancellationToken cancellationToken);
    }
}
