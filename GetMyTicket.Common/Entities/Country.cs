using System;
using System.ComponentModel.DataAnnotations;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class Country
    {
        public Guid CountryId { get; set; } = Guid.CreateVersion7();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<City> Destinations { get; set; } 

    }
}