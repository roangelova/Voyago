using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.User
{
    public class CreateUserDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool NewsletterSubscribtion { get; set; } = true;
    }
}
