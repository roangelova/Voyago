using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities.Passengers
{
    public class Adult : Passenger
    {
        public required User User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public DocumentType DocumentType { get; set; }

        [MaxLength(MaxDocumentIdLength)]
        public required string DocumentId{ get; set; }

        public DateOnly ExpirationDate { get; set; }

        //TODO: SeatNumber will be not-required until we add the functionality to the frontend
        public string? SeatNumber { get; set; }
    }
}
