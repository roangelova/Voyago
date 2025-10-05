using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class Booking : ITrackableEntity
    {
        public Guid Id { get; set; }

        //this is the id of the booking we share with the customers for an easier identification of their booking. 
        [MaxLength(BookingReferenceMaxLength)]
        public string Reference { get; set; }

        public double TotalPrice { get; set; }

        public double TotalDiscountUsed { get; set; }

        public Discount Discount { get; set; }

        [ForeignKey(nameof(Discount))]
        public Guid? DiscountId { get; set; }

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
