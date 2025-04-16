using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class TransportationProvider :ITrackableEntity
    {
        public Guid TransportationProviderId { get; set; } 

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        public byte[] Logo { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Trip> Trips { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }

    }
}
