using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageItemsController : ControllerBase
    {
        private readonly IBaggageItemService baggageItemService;

        public BaggageItemsController(IBaggageItemService baggageItemService)
        {
            this.baggageItemService = baggageItemService;
        }
    }
}
