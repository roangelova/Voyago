namespace GetMyTicket.Common.DTOs.User
{
    public record RegisterUserDTO(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Dob,
        bool NewsletterSubscribtion,
        string Address
        );

}
