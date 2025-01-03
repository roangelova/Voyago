using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;
using System.ComponentModel.DataAnnotations;

using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class TransportationProvider
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Trip> Trips { get; set; }

    }
}
