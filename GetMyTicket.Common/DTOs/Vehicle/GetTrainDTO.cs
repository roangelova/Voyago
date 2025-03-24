namespace GetMyTicket.Common.DTOs.Vehicle
{
    public class GetTrainDTO
    {
        public int Capacity { get; set; }

        public Guid TransportationProviderId { get; set; }

        public Guid TrainId { get; set; }

        public string TransportationpProviderName { get; set; }
    }
}
