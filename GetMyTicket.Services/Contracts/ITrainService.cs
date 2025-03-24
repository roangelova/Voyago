using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Common.Entities.Vehicles;

namespace GetMyTicket.Service.Contracts
{
    public interface ITrainService
    {
        public Task<GetTrainDTO> GetById(object Id);
        public Task<GetTrainDTO> Add (AddTrainDTO trainDTO);
    }
}
