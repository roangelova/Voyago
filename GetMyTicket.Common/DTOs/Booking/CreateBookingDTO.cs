using GetMyTicket.Common.DTOs.Baggage;

namespace GetMyTicket.Common.DTOs.Booking
{
    public class CreateBookingDTO
    {
        public Guid TripId { get; set; }
        public List<Guid> PassengerIds { get; set; }

        public List<BaggageItemDTO> Baggage { get; set; }
        public Guid UserId { get; set; }
    }
}
