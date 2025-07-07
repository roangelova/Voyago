using GetMyTicket.Common.DTOs.City;
using GetMyTicket.Common.Entities;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CityDTO>> GetAll()
        {
            var cities = await unitOfWork.Cities.GetAllAsync();

            return cities.Select(x => new CityDTO
            {
                CityName = x.CityName,
                IATA = x.IATA_Code,
                Id = x.Id
            }).ToList();
        }
    }
}
