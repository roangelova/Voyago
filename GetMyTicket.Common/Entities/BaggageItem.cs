using GetMyTicket.Common.Entities.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class BaggageItem
    {
        public Guid BaggageItemId { get; set; } 

        [Required]
        [Range(0, MaxBaggageSize)]
        public int Size { get; set; }

        public Passenger Passenger { get; set; }

        [ForeignKey(nameof(Passenger))]
        [Required]
        public Guid PassengerId { get; set; }

        public Trip Trip { get; set; }

        [ForeignKey(nameof(Trip))]
        [Required]
        public Guid TripId { get; set; }
    }
}
