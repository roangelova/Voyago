using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Common.Entities;

namespace GetMyTicket.Service.Contracts
{
    public interface ITransportationProviderService
    {
        //GET
        public Task<IEnumerable<GetTransportationProviderDTO>> GetAll();
        public Task<GetTransportationProviderDTO> GetById(object Id);

        //POST
        public Task<TransportationProvider> Add( CreateTransportationProviderDTO addTpDTO);

        //PUT
        public Task<TransportationProvider> Update(object id, EditTransportationProvider editTransportationProvider);

        //DELETE
    }
}
