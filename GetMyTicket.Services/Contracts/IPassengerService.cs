using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.DTOs.Passenger;
using GetMyTicket.Common.Entities.Contracts;

namespace GetMyTicket.Service.Contracts
{
    public interface IPassengerService
    {
        public Task<GetPassengerDTO> GetPassengerAsync(Guid userId);
        public Task<Guid> CreatePassenger(CreateOrEditPassengerDTO createOrEditPassengerDTO);
        public Task<GetPassengerDTO> EditPassenger(Guid passengerId, CreateOrEditPassengerDTO createOrEditPassengerDTO);
    }
}
