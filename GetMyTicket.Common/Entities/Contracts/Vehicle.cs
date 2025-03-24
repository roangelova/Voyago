using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Vehicle
    {
        public Guid VehicleId { get; set; }

        [Range(1, MaxVehicleCapacity)]
        public int Capacity {  get; set; }      

        public TransportationProvider TransportationProvider {  get; set; }

        [ForeignKey(nameof(TransportationProvider))]
        [Required]
        public Guid TransportationProviderId { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}
