using GetMyTicket.Common.DTOs;

namespace GetMyTicket.Service.Contracts
{
    public interface IPassengerService
    {
        public Task<Guid> GetPassengerIdAsync(Guid userId);
        public Task<Guid> CreatePassengerData(BookTripDTO bookTripDTO);
    }
}
