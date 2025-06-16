using GetMyTicket.Common.Entities.Trackable;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class BaggageItem : ITrackableEntity
    {
        public Guid BaggageItemId { get; set; }

        [Range(0, MaxBaggageSize)]
        public int Size { get; set; }

        public required Passenger Passenger { get; set; }

        [ForeignKey(nameof(Passenger))]
        public Guid PassengerId { get; set; }

        public required Trip Trip { get; set; }

        [ForeignKey(nameof(Trip))]
        public Guid TripId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
