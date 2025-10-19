using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Discount;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork unitOfWork;

        public DiscountService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> CanApplyDiscountToBooking(Guid passengerId, string discountName, decimal bookingCurrentTotal)
        {
            var discount = await unitOfWork.Discounts.GetAsync(x => x.Name.ToUpper() == discountName.ToUpper());

            if (discount is null)
            {
                throw new ApplicationError(ResponseConstants.InvalidDiscount);
            }

            if(discount.ExpirationDate <  DateTime.UtcNow)
            {
                throw new ApplicationError(ResponseConstants.DiscountExpired);
            }

            if(bookingCurrentTotal <= discount.MinimumAmount)
            {
                throw new ApplicationError(ResponseConstants.CantApplyDiscount);
            }

            //TODO -> IF WE ADD GLOBAL QUERY FILTER, REMOVE IT SO THAT WE CAN CHECK EVEN IF PAST BOOKINGS HAVE USED IT 
            var passengerBookings = await unitOfWork.PassengerBookingMap.GetAllAsync(x => x.PassengerId == passengerId, null, false, default, x=> x.Booking);
            bool HasUsedDiscountCode = passengerBookings.Any( x=> x.Booking.DiscountId == discount.Id);

            if(HasUsedDiscountCode)
            {
                throw new ApplicationError(ResponseConstants.DiscountHasBeenUsed);
            }

            return true;
        }

        public async Task<Guid> CreateDiscount(CreateDiscountDTO dto)
        {
            bool IsValidType = Enum.TryParse<DiscountType>(dto.DiscountType, out var discountType );

            if(!IsValidType)
            {
                throw new ApplicationError(string.Format(ResponseConstants.InvalidType, dto.DiscountType, nameof(DiscountType)));
            }

            var discount = new Discount
            {
                Name = dto.Name.Trim().ToUpper(),
                DiscountType = discountType,
                Value = dto.Value,
                MinimumAmount = dto.MinimumAmount,
                IsActive = true,
                HasExpirationDate = dto?.ExpirationDate is not null ? true : false,
                ExpirationDate = dto.ExpirationDate ?? null
            };

            await unitOfWork.Discounts.AddAsync(discount);
            await unitOfWork.SaveChangesAsync();

            return discount.Id;
        }

        public async Task<GetDiscountDTO> GetDiscount(string discountName)
        {
            var discount = await unitOfWork.Discounts.GetAsync(x => x.Name.ToUpper() == discountName.ToUpper());

            if (discount is null)
            {
                throw new ApplicationError(ResponseConstants.InvalidDiscount);
            }

            return new GetDiscountDTO
            {
                Id = discount.Id,
                Name = discount.Name,
                DiscountType = Enum.GetName(discount.DiscountType),
                Value = discount.Value, 
                HasExpirationDate = discount.HasExpirationDate,
                ExpirationDate = discount?.ExpirationDate ?? null
            };
        }
    }
}
