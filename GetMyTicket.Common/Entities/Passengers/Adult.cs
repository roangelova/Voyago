using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities.Passengers
{
    public class Adult : Passenger
    {
        public User User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(MaxDocumentIdLength)]
        public string DocumentId{ get; set; }

        [Required]
        public DateOnly ExpirationDate { get; set; }

        public string SeatNumber { get; set; }
    }
}
