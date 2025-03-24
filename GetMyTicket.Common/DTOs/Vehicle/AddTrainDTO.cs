namespace GetMyTicket.Common.DTOs.Vehicle
{
    public class AddTrainDTO
    {
        public int Capacity { get; set; }

        public Guid TransportationProviderId { get; set; }

        public bool HasBistroOnBoard { get; set; }

        public bool IsAHighspeedTrain { get; set; }
    }
}
