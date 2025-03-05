using GetMyTicket.Common.DTOs;

namespace GetMyTicket.Service.Contracts
{
    public interface IBookingService
    {
        public Task<Guid> CreateBooking(CreateBookingDTO bookTripDTO);
    }
}
