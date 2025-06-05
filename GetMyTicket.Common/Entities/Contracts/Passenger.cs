using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Passenger : ITrackableEntity
    {
        public Guid PassengerId { get; set; }

        [MaxLength(NameMaxLength)]
        public required string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string? MiddleName { get; set; }

        [MaxLength(NameMaxLength)]
        public required string LastName { get; set; }
        public Gender Gender { get; set; }

        public ICollection<PassengerBookingMap> PassengerBookingMap { get; set; } = [];

        [MaxLength(MaxNationalityLength)]
        public required string Nationality { get; set; }

        public DateOnly DOB { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
