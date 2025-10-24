using System.Buffers.Text;
using GetMyTicket.Common.DTOs.Vehicle;

namespace GetMyTicket.Common.DTOs.Trip
{
    public class TripSearchResultDTO
    {
        public Guid TripId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildrenPrice { get; set; }
        public string EndCityName { get; set; }
        public string StartCityName { get; set; }
        public string TransportationProviderName { get; set; }

        public string TransportationProviderId { get; set; }

        public string? TransportationProviderLogo { get; set; }
        public string Currency { get; set; }

        public string TypeOfTrip { get; set; }

        public AmenitiesViewModel Amenities {  get; set; }
    }
}
