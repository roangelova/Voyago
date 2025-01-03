using GetMyTicket.Common.Enum;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Passenger
    {
        public Gender Gender { get; set; }
        public ICollection<Trip>? PassengerTrips { get; set; }
    }
}
