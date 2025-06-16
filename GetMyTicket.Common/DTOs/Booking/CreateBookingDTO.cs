namespace GetMyTicket.Common.DTOs.Booking
{
    public class CreateBookingDTO
    {
        public Guid TripId { get; set; }
        public List<CreateBooking_PassengerIdAndTypeDTO> Passengers { get; set; }
        public Guid UserId { get; set; }
    }
}
