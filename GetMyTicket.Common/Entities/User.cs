using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Mapping_Tables;
using Microsoft.AspNetCore.Identity;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class User : IdentityUser<Guid>, ITrackableEntity
    {
        [MaxLength(NameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string? LastName { get; set; }

        public override string? UserName { get; set; }

        public override string? Email { get; set; }

        public DateOnly? DOB {  get; set; }

        public bool IsSubscribedForNewsletter { get; set; } = true;

        [MaxLength(AddressMaxLength)]
        public string? Address { get; set; }

        public ICollection<UserPassengerMap> UserPassengerMaps { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
