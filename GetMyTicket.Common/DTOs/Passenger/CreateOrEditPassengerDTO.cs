namespace GetMyTicket.Common.DTOs.Passenger
{
    public class CreateOrEditPassengerDTO
    {
        public Guid? UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Dob {  get; set; }

        public string Nationality { get; set; }

        public string DocumentType { get; set; }

        public string DocumentId { get; set; }

        public string DocumentExpirationDate { get; set; }
    }
}
