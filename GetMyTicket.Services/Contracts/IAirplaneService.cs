using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Common.Entities.Vehicles;

namespace GetMyTicket.Service.Contracts
{
    public interface IAirplaneService
    {
        public Task<GetAirplaneDTO> GetById (object id);

        public Task<Airplane> Add (AddAirplaneDTO airplaneDTO);
    }
}
