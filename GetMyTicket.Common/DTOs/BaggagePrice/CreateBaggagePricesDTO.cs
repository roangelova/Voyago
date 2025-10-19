using GetMyTicket.Common.Enum;

namespace GetMyTicket.Common.DTOs.BaggagePrice
{
    public class CreateBaggagePricesDTO
    {
        public Guid TransportationProviderId { get; set; }

        public Dictionary<BaggageSize, decimal> Prices { get; set; }
    }
}
