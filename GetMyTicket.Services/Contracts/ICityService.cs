using GetMyTicket.Common.DTOs.City;
using GetMyTicket.Common.Entities;

namespace GetMyTicket.Service.Contracts
{
    public interface ICityService
    {
        public Task<IEnumerable<CityDTO>> GetAll();
    }
}
