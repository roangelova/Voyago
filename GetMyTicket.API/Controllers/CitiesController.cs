using GetMyTicket.Common.Entities;
using GetMyTicket.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GetMyTicket.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : Controller
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<City>> GetAll() 
        {
           var result =  await cityService.GetAll(true);

            return result;
        }

    }
}
