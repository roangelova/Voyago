using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;

namespace GetMyTicket.Common.Entities
{
    public class BaggagePrice : ITrackableEntity
    {
        public Guid Id { get; set; }

        public Guid TransportationProviderId { get; set; }

        public TransportationProvider TransportationProvider { get; set; }

        public BaggageSize BaggageSize { get; set; }

        public double Price { get; set; }

        public Currency Currency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
