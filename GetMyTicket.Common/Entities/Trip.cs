using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities
{
    public class Trip
    {
        public Guid TripId { get; set; }

        public TypeOfTransportation TypeOfTransportation { get; set; }

        public TransportationProvider TransportationProvider { get; set; }

        [ForeignKey(nameof(TransportationProvider))]
        public Guid TransportationProviderId { get; set; }

        public Vehicle Vehicle { get; set; }

        [Required]
        public City StartCity { get; set; }

        [ForeignKey(nameof(StartCity))]
        public Guid StartCityId { get; set; }

        [Required]
        public City EndCity { get; set; }

        [ForeignKey(nameof(EndCity))]
        public Guid EndCityId { get; set; }

        //TODO -> what was I thinking, this does not work like this!! Fix this issue along with other small db changes
        public int Capacity => 130;

        [Required]
        [ForeignKey(nameof(Vehicle))]
        public Guid VehicleId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public double Price { get; set; }

        public Currency Currency { get; set; } 

        public ICollection<Booking> Bookings { get; set; }

    }
}
