using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.DTOs.Passenger;

namespace GetMyTicket.Service.Contracts
{
    public interface IPassengerService
    {
        public Task<Guid> GetPassengerIdAsync(Guid userId);
        public Task<Guid> CreatePassengerData(MapPassengerDTO mapPassengerDTO);
    }
}
