using GetMyTicket.Common.DTOs.Baggage;

namespace GetMyTicket.Service.Contracts
{
    public interface IBaggageItemService
    {
        public Task<List<BaggageItemDTO>> GetBaggageItemsForBooking(Guid bookingId, CancellationToken cancellationToken);
    }
}
