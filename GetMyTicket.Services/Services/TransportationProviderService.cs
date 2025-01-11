using GetMyTicket.Common.DTOs;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class TransportationProviderService : ITransportationProviderService
    {
        private readonly IUnitOfWork unitOfWork;

        public TransportationProviderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll()
        {
            var data = await unitOfWork.TransportationProviders.GetAllAsync();

            return data.Select(x => new GetTransportationProviderDTO(

               x.TransportationProviderId.ToString(),
                 x.Name,
                x.Description
            ));
        }
    }
}
