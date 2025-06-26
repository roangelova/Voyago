using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Trackable;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Mapping_Tables
{
    [PrimaryKey("PassengerId", "BookingId")]
    public class PassengerBookingMap : ITrackableEntity
    {
        public Passenger Passenger { get; set; }

        [ForeignKey(nameof(Passenger))]
        public Guid PassengerId { get; set; }

        public Booking Booking { get; set; }

        [ForeignKey(nameof(Booking))]
        public Guid BookingId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
