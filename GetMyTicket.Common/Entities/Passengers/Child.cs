using GetMyTicket.Common.Entities.Contracts;

namespace GetMyTicket.Common.Entities.Passengers
{
    public class Child : Passenger
    {
        //TODO: SeatNumber will be not-required until we add the functionality to the frontend
        public string? SeatNumber { get; set; }
    }
}
