using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Baggage;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class BaggageItemService : IBaggageItemService
    {
        private readonly IUnitOfWork unitOfWork;

        public BaggageItemService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;  
        }

        public async Task<List<BaggageItemDTO>> GetBaggageItemsForBooking(Guid bookingId, CancellationToken cancellationToken)
        {
           var booking = await unitOfWork.Bookings.GetAsync(x => x.BookingId ==bookingId, true, cancellationToken, x => x.BaggageItems);

            if(booking is  null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Booking), bookingId));
            }

            var grouped = booking.BaggageItems.GroupBy( x=> x.Size).ToList();

            var result = new List<BaggageItemDTO>();

            foreach (var bg in grouped)
            {
                result.Add(new BaggageItemDTO
                {
                    Amount = bg.Count(),
                    Type = Enum.GetName<BaggageSize>(bg.Key)
                });
            }

            return result;
        }
    }
}
