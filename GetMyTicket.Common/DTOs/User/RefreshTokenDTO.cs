using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.User
{
    public class RefreshTokenDTO
    {
        [Required]
        public string RefreshToken { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
