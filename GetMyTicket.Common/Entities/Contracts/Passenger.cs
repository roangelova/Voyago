using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities.Contracts
{
    public abstract class Passenger
    {
        public Guid PassengerId { get; set; }
        public Gender Gender { get; set; }

        public ICollection<PassengerBookingMap> PassengerBookingMap { get; set; }
    }
}
