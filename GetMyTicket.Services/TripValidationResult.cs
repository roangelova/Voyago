using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Enum;

namespace GetMyTicket.Service
{
    public class TripValidationResult
    {
        public bool IsValid => ErrorMessage == null;
        public string ErrorMessage { get; set; }

        public Vehicle Vehicle { get; set; }
        public TransportationProvider Provider { get; set; }
        public TypeOfTransportation TransportationType { get; set; }
    }
}
