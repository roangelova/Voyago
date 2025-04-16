using System.ComponentModel.DataAnnotations;
using GetMyTicket.Common.Entities.Trackable;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class City : ITrackableEntity
    {
        public Guid CityId { get; set; } = Guid.CreateVersion7();

        [Required]
        [MaxLength(NameMaxLength)]
        public string CityName { get; set; }

        [Required]
        [MaxLength(IATA_CodeMaxLength)]
        public string IATA_Code { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedAt { get; set; }
    }
}
