using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.User
{
    public class RefreshTokenDTO
    {
        [Required]
        public string RefreshToken { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
    }
}
