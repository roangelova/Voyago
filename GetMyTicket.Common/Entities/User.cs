using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static GetMyTicket.Common.Constants.EntityConstraintsConstants;

namespace GetMyTicket.Common.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public override string UserName { get; set; }

        public DateOnly? DOB {  get; set; }

        public bool IsSubscribedForNewsletter { get; set; } = true;

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public DateOnly RegistrationDate { get; set; }

    }
}
