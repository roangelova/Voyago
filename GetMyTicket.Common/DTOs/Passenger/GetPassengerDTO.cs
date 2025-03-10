namespace GetMyTicket.Common.DTOs.Passenger
{
    public class GetPassengerDTO
    {
        public Guid UserId { get; set; }

        public Guid PassengerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string DocumentType { get; set; }

        public string DocumentId { get; set; }

        public DateOnly Dob {  get; set; }

        public string Nationality { get; set; }
    }
}
