using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Mapping_Tables
{
    //NOTE: A Passenger can be shared between user accounts (for example, 2 parents can both hold an account and book for their child
    public class UserPassengerMap : ITrackableEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        [MaxLength(MaxPassengerLabelLength)]
        public string? Label { get; set; }
        public bool IsAccountOwner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
