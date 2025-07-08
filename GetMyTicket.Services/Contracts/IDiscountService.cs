using GetMyTicket.Common.DTOs.Discount;

namespace GetMyTicket.Service.Contracts
{
    public interface IDiscountService
    {
        public Task<GetDiscountDTO> GetDiscount(Guid discountId);

        public Task<Guid> CreateDiscount(CreateDiscountDTO dto);

        public Task<bool> CanApplyDiscountToBooking(Guid passengerId, Guid discountId);
    }
}
