using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class TransportationProvider :ITrackableEntity
    {
        public Guid TransportationProviderId { get; set; } 

        [MaxLength(NameMaxLength)]
        public required string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public required string Description { get; set; }

        [MaxLength(AddressMaxLength)]
        public  required string Address { get; set; }

        [MaxLength(EmailMaxLength)]
        public  required string Email { get; set; }

        public byte[] Logo { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = [];
        public ICollection<Trip> Trips { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }

    }
}
