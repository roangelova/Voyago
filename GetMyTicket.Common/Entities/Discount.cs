using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;

namespace GetMyTicket.Common.Entities
{
    public class Discount : ITrackableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DiscountType DiscountType { get; set; }

        public bool HasExpirationDate { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];

        public DateTime? ExpirationDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
