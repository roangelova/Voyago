namespace GetMyTicket.Common.DTOs
{
    public class CreateBookingDTO
    {
        public Guid TripId { get; set; }
        public Guid UserId { get; set; }
    }
}
