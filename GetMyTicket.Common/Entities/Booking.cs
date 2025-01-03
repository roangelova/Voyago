using GetMyTicket.Common.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities
{
    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.CreateVersion7();

        public Trip Trip { get; set; }

        [ForeignKey(nameof(Trip))]
        public Guid TripId { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}
