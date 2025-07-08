using GetMyTicket.Common.DTOs.Discount;

namespace GetMyTicket.Service.Contracts
{
    public interface IDiscountService
    {
        public Task<GetDiscountDTO> GetDiscount(string discountName);

        public Task<Guid> CreateDiscount(CreateDiscountDTO dto);

        public Task<bool> CanApplyDiscountToBooking(Guid passengerId, string discountName);
    }
}
