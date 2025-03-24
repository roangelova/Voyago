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
        public string StartTime { get; set; }

        [Required]
        public string EndTime { get; set; }

        [Range(0, 9999)]
        public double Price { get; set; }
    }
}
