namespace GetMyTicket.Common.DTOs.Booking
{
    public class ListBookingDTO
    {
        public Guid BookingId { get; set; }

        public string ToCityName { get; set; }

        public string FromCityName { get; set; }

        public DateTime DepartureTime { get; set; }

        public double TotalPrice { get; set; }
    }
}
