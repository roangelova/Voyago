using GetMyTicket.Common.Entities.Contracts;

namespace GetMyTicket.Common.Entities.Vehicles
{
    public class Train : Vehicle
    {
        public bool HasBistroOnBoard { get; set; }

        public bool IsAHighspeedTrain { get; set; }
    }
}
