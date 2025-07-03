using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Vehicle : ITrackableEntity
    {
        public Guid Id { get; set; }

        [Range(1, MaxVehicleCapacity)]
        public int Capacity { get; set; }

        public TransportationProvider TransportationProvider { get; set; }

        [ForeignKey(nameof(TransportationProvider))]
        public Guid TransportationProviderId { get; set; }

        public ICollection<Trip> Trips { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
