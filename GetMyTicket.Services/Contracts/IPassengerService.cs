using GetMyTicket.Common.DTOs.Passenger;

namespace GetMyTicket.Service.Contracts
{
    public interface IPassengerService
    {
        public Task<List<GetPassengerDTO>> GetPassengerAsync(Guid userId, CancellationToken cancellationToken);
        public Task<Guid> CreatePassenger(CreatePassengerDTO createOrEditPassengerDTO);
        public Task<GetPassengerDTO> EditPassenger(EditPassengerDTO createOrEditPassengerDTO, CancellationToken cancellationToken);

        public Task<List<GetNameAndAgePassengerDataDTO>> GetPassengersForBooking(Guid bookingId, CancellationToken cancellationToken = default);

        public Task DeletePassenger (Guid passengerId, CancellationToken cancellationToken);
    }
}
