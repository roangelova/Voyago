using GetMyTicket.Common.Entities.Trackable;
using Microsoft.AspNetCore.Identity;

namespace GetMyTicket.Common.Entities
{
    public class ApplicationRole: IdentityRole<Guid>, ITrackableEntity
    {
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
