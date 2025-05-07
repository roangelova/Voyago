using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.DTOs.Booking;

namespace GetMyTicket.Service.Contracts
{
    public interface IBookingService
    {
        public Task<Guid> CreateBooking(CreateBookingDTO bookTripDTO);

        public Task<IEnumerable<ListBookingDTO>> GetUserBookings(Guid userId, CancellationToken cancellationToken = default);

        public Task<int> CancelBooking (object bookingId);
    }
}
