using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.TP;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.ErrorHandling;
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

        public async Task<TransportationProvider> Add(CreateTransportationProviderDTO addTpDTO)
        {
            if (string.IsNullOrWhiteSpace(addTpDTO.Name) ||
                string.IsNullOrWhiteSpace(addTpDTO.Description) ||
                string.IsNullOrWhiteSpace(addTpDTO.Email) ||
                string.IsNullOrWhiteSpace(addTpDTO.Address))
            {
                throw new ApplicationError(ErrorMessages.AllFieldsRequired);
            }

            byte[] logo = null;

            if (addTpDTO.Logo != null)
            {
                using (var stream = new MemoryStream())
                {
                    await addTpDTO.Logo.CopyToAsync(stream);
                    logo = stream.ToArray();
                }
            }

            var entity = new TransportationProvider
            {
                TransportationProviderId = Guid.CreateVersion7(),
                Name = addTpDTO.Name.Trim(),
                Description = addTpDTO.Description.Trim(),
                Address = addTpDTO.Address.Trim(),
                Email = addTpDTO.Email.Trim(),
                Logo = logo
            };

            await unitOfWork.TransportationProviders.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<GetTransportationProviderDTO>> GetAll()
        {
            var data = await unitOfWork.TransportationProviders.GetAllAsync();

            return data.Select(x => new GetTransportationProviderDTO(

               x.TransportationProviderId.ToString(),
                 x.Name,
                x.Description,
                Convert.ToBase64String(x.Logo)
            ));
        }

        public async Task<GetTransportationProviderDTO> GetById(object Id)
        {
            var entity = await unitOfWork.TransportationProviders.GetByIdAsync(Id);

            if (entity != null)
            {
                return new GetTransportationProviderDTO(
                    entity.TransportationProviderId.ToString(),
                    entity.Name,
                    entity.Description, 
                    Convert.ToBase64String(entity.Logo)
                    );
            }

            return null;
        }
    }
}
