using System.ComponentModel.DataAnnotations;

namespace GetMyTicket.Common.DTOs.Passenger
{
    public class CreatePassengerDTO
    {
        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateOnly Dob {  get; set; }

        public string? Label { get; set; }

        public bool IsAccountOwner { get; set; }

        public string? Nationality { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentId { get; set; }

        public DateOnly? DocumentExpirationDate { get; set; }
    }
}
