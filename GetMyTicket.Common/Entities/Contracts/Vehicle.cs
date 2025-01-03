using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Vehicle
    {
        public Guid VehicleId { get; set; } = Guid.CreateVersion7();

        public int Capacity {  get; set; }      

        public TransportationProvider TransportationProvider {  get; set; }

        [ForeignKey(nameof(TransportationProvider))]
        [Required]
        public Guid TransportationProvideriD { get; set; }
    }
}
