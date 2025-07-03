using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class Country :ITrackableEntity
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();

        [Required]
        [MaxLength(NameMaxLength)]
        public required string Name { get; set; }

        public ICollection<City> Destinations { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}