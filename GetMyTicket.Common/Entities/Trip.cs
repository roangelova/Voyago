using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities
{
    public class Trip: ITrackableEntity
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

        public int Capacity { get; set; }

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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
