using GetMyTicket.Common.DTOs.Discount;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService discountService;

        public DiscountsController(IDiscountService discountService)
        {
            this.discountService = discountService;
        }

        [HttpGet("{id}")]
        public async Task<GetDiscountDTO> GetDiscount(Guid id)
        {
            return await discountService.GetDiscount(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDTO dto)
        {
            Guid id = await discountService.CreateDiscount(dto);

            return CreatedAtAction(nameof(GetDiscount), new { id}, id);
        }

        /// <summary>
        /// Checks if the discount can be applied to a booking currently in progress by passenger with the provided Id. Each discount code can be used only
        /// once per user account and booking. Some discouunt codes also have an expiration date.
        /// </summary>
        /// <param name="passengerId"></param>
        /// <param name="discountId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CanApplyDiscountToBooking(Guid passengerId, Guid discountId)
        {
            return await discountService.CanApplyDiscountToBooking(passengerId, discountId);
        }
    }
}
