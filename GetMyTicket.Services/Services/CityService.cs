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

        public async Task<IEnumerable<City>> GetAll(bool asNoTracking)
        {
            return await unitOfWork.Cities.GetAllAsync(AsNonTracking: asNoTracking);
        }
    }
}
