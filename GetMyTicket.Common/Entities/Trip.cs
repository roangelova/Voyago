using System.ComponentModel.DataAnnotations.Schema;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public City StartCity { get; set; }

        [ForeignKey(nameof(StartCity))]
        public Guid StartCityId { get; set; }

        public City EndCity { get; set; }

        [ForeignKey(nameof(EndCity))]
        public Guid EndCityId { get; set; }

        public int Capacity { get; set; }

        [ForeignKey(nameof(Vehicle))]
        public Guid VehicleId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double AdultPrice { get; set; }

        public double ChildrenPrice { get; set; }

        //infants travel free of charge, therefore no seperate prop was created

        public Currency Currency { get; set; } 

        public ICollection<Booking> Bookings { get; set; } = [];

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
