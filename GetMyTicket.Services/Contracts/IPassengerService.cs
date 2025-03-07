using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Common.Entities.Contracts;

namespace GetMyTicket.Service.Contracts
{
    public interface IPassengerService
    {
        public Task<Guid> GetPassengerIdAsync(Guid userId);
        public Task<Guid> CreatePassenger(CreateOrEditPassengerDTO createOrEditPassengerDTO);
        public Task<GetPassengerDTO> EditPassenger(Guid passengerId, CreateOrEditPassengerDTO createOrEditPassengerDTO);
    }
}
