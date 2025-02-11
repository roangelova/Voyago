using GetMyTicket.Common.DTOs;

namespace GetMyTicket.Service.Contracts
{
    public interface IBookingService
    {
        public Task<Guid> BookTrip(BookTripDTO bookTripDTO);
    }
}
