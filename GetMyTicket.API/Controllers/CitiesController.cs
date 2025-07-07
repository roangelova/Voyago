using GetMyTicket.Common.DTOs.City;
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

        [HttpGet]
        public async Task<IEnumerable<CityDTO>> GetAll() 
        {
           return await cityService.GetAll();
        }

    }
}
