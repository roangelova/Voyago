using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.Trip
{
    public class UpdateTripDTO
    {
        [Required]
        public Guid? TripId {  get; set; }

        public Guid? VehicleId { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        [Required]
        [Range(0, 9999)]
        public double? Price { get; set; }
    }
}
