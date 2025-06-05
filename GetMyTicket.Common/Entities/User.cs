using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Passengers;
using GetMyTicket.Common.Entities.Trackable;
using Microsoft.AspNetCore.Identity;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class User : IdentityUser<Guid>, ITrackableEntity
    {
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public override string UserName { get; set; }

        public DateOnly? DOB {  get; set; }

        public bool IsSubscribedForNewsletter { get; set; } = true;

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public DateOnly RegistrationDate { get; set; }

        public Adult PassengerMap { get; set; }

        [ForeignKey(nameof(Passenger))]
        public Guid PassengerMapId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
