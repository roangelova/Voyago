using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;

namespace GetMyTicket.Common.Entities
{
    public class Booking : ITrackableEntity
    {
        public Guid BookingId { get; set; }

        public double TotalPrice { get; set; }

        public Trip Trip { get; set; }

        [ForeignKey(nameof(Trip))]
        public Guid TripId { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public ICollection<BaggageItem> BaggageItems { get; set; } = [];

        public ICollection<PassengerBookingMap> PassengerBookingMap { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
