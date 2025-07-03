using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    /// <summary>
    /// Nationality, doc type, doc id and doc expiration date are only applicable for flights. Furthermore, passengers can always check-in at the airport and provide the information then. 
    /// </summary>
    public class Passenger : ITrackableEntity
    {
        public Guid Id { get; set; }

        public PassengerType PassengerType { get; set; }

        [MaxLength(NameMaxLength)]
        public required string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string? MiddleName { get; set; }

        [MaxLength(NameMaxLength)]
        public required string LastName { get; set; }
        public Gender Gender { get; set; }

        public ICollection<UserPassengerMap> UserPassengerMaps { get; set; } = [];

        public ICollection<PassengerBookingMap> PassengerBookingMaps { get; set; } = [];

        [MaxLength(MaxNationalityLength)]
        public string? Nationality { get; set; }

        public DocumentType? DocumentType { get; set; }

        [MaxLength(MaxDocumentIdLength)]
        public string? DocumentId { get; set; }

        public DateOnly? ExpirationDate { get; set; }

        public DateOnly DOB { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
