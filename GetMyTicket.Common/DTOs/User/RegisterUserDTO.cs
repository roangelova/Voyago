namespace GetMyTicket.Common.DTOs.User
{
    public record CreateUserDTO(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Dob,
        bool NewsletterSubscribtion,
        string Address
        );

}
