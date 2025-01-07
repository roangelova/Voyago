using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities
{
    public class Booking
    {
        public Guid BookingId { get; set; } 

        public double TotalPrice { get; set; }

        public Trip Trip { get; set; }

        [ForeignKey(nameof(Trip))]
        public Guid TripId { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public ICollection<BaggageItem> BaggageItems { get; set; }

        public ICollection<PassengerBookingMap> PassengerBookingMap { get; set; }

    }
}
