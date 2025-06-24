namespace GetMyTicket.Common.DTOs.Passenger
{
    public class GetPassengerDTO
    {
        public Guid PassengerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PassengerType { get; set; }

        public string Gender { get; set; }

        public string DocumentType { get; set; }

        public DateOnly? DocumentExpirationDate { get; set; }

        public string DocumentId { get; set; }

        public string? Label { get; set; }

        public DateOnly Dob {  get; set; }

        public string Nationality { get; set; }

        public bool IsAccountOwner { get; set; }
    }
}
