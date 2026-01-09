using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;
namespace GetMyTicket.Common.Entities
{
    public class Notification : ITrackableEntity
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [MaxLength(NotificationTitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(NotificationContentMaxLength)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}

//TODO: 
// CREATE NOTIFICATIONS TABLE ON THE FRONTEND 
//notifications should be fetched as part of the accountContext
//send a Discount code as notification as soon as the user registers