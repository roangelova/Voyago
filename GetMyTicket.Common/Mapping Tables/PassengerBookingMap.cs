using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Mapping_Tables
{
    [PrimaryKey("PassengerId", "BookingId")]
    public class PassengerBookingMap
    {
        public Passenger Passenger { get; set; }

        [ForeignKey(nameof(Passenger))]
        public Guid PassengerId { get; set; }

        public Booking Booking { get; set; }

        [ForeignKey(nameof(Booking))]
        public Guid BookingId { get; set; }
    }
}
