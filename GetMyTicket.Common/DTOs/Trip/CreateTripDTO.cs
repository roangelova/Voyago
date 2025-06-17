using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.Trip
{
    public class CreateTripDTO
    {
        public string TypeOfTransportation { get; set; }

        [Required]
        public Guid TransortationProviderId { get; set; }

        [Required]
        public Guid StartCityId { get; set; }

        [Required]
        public Guid EndCityId { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public DateTime? StartTime { get; set; }

        [Required]
        public DateTime? EndTime { get; set; }

        [Required]
        [Range(0, 9999)]
        public double? AdultPrice { get; set; }

        [Required]
        [Range(0, 9999)]
        public double? ChildrenPrice { get; set; }
    }
}
