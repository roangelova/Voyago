using GetMyTicket.Common.Enum;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unitOfWork;

        public JobService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ArchivePastBookings()
        {
            var bookingsWithDatesInThePast =
                await unitOfWork.Bookings.GetAllAsync(x =>
                x.Trip.EndTime < DateTime.UtcNow,
                null,
                false,
                default,
                x => x.Trip
                );

            foreach (var booking in bookingsWithDatesInThePast)
            {
                booking.BookingStatus = BookingStatus.Archived;
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}
