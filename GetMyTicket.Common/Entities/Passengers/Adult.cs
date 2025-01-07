using GetMyTicket.Common.Entities.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Common.Entities.Passengers
{
    public class Adult : Passenger
    {
        public User User { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; } 
    }
}
