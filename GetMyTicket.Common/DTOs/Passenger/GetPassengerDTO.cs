namespace GetMyTicket.Common.DTOs.Passenger
{
    public class GetPassengerDTO
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid PassengerId { get; set; }
    }
}
