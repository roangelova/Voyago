namespace GetMyTicket.Common.DTOs.Passenger
{
    public class EditPassengerDTO
    {
        public Guid PassengerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Label { get; set; }

        public string? Nationality { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentId { get; set; }

        public DateOnly? DocumentExpirationDate { get; set; }
    }
}
