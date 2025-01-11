using GetMyTicket.Common.DTOs;

namespace GetMyTicket.Service.Contracts
{
    public interface ITransportationProviderService
    {
        public Task<IEnumerable<GetTransportationProviderDTO>> GetAll();
    }
}
