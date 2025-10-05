namespace GetMyTicket.Common.DTOs.Booking
{
    public class ListBookingDTO
    {
        public Guid BookingId { get; set; }

        public string Reference {  get; set; }

        public string ToCityName { get; set; }

        public string FromCityName { get; set; }

        public DateTime DepartureTime { get; set; }

        public double TotalPrice { get; set; }

        public double TotalDiscountUsed { get; set; }

        public string Currency {  get; set; }

        public string Status { get; set; }

        public DateTime BookingDate { get; set; }

        public Guid TripId { get; set; }
    }
}
