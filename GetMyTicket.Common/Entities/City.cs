using System.ComponentModel.DataAnnotations;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class City
    {
        public Guid CityId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string CityName { get; set; }

        [Required]
        [MaxLength(IATA_CodeMaxLength)]
        public string IATA_Code {get; set; }

    }
}
