using GetMyTicket.Common.Entities.Contracts;

namespace GetMyTicket.Common.Entities.Passengers
{
    public class Infant : Passenger
    {
        public bool TravellingWithAStroller { get; set; } = false;
    }
}
