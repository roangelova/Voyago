using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Passenger : ITrackableEntity
    {
        public Guid PassengerId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }
        public Gender Gender { get; set; }

        public ICollection<PassengerBookingMap> PassengerBookingMap { get; set; }

        [Required]
        [MaxLength(MaxNationalityLength)]
        public string Nationality { get; set; }

        [Required]
        public DateOnly DOB { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
