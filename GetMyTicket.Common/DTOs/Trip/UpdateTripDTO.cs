using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.Trip
{
    public class UpdateTripDTO
    {
        [Required]
        public Guid? TripId {  get; set; }

        public Guid? VehicleId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Range(0, 9999)]
        public decimal? AdultPrice { get; set; }

        [Required]
        [Range(0, 9999)]
        public decimal? ChildrenPrice { get; set; }
    }
}
