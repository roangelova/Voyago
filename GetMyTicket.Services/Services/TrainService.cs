using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class TrainService : ITrainService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrainService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetTrainDTO> Add(AddTrainDTO trainDTO)
        {
            if (trainDTO.Capacity <= 0)
            {
                throw new ApplicationException(string.Format(ResponseConstants.CantBeNull, nameof(trainDTO.Capacity)));
            }

            var TransportationProvider = await unitOfWork.TransportationProviders.GetByIdAsync(trainDTO.TransportationProviderId);

            if (TransportationProvider == null)
            {
                throw new ApplicationException(string.Format(ResponseConstants.NotFoundError,
                    nameof(TransportationProvider),
                    trainDTO.TransportationProviderId));
            }

            Train entity = new()
            {
                VehicleId = Guid.CreateVersion7(),
                Capacity = trainDTO.Capacity,
                HasBistroOnBoard = trainDTO.HasBistroOnBoard,
                IsAHighspeedTrain = trainDTO.IsAHighspeedTrain,
                TransportationProviderId = trainDTO.TransportationProviderId,
                TransportationProvider = TransportationProvider
            };

            await unitOfWork.Vehicles.AddAsync(entity);
            await unitOfWork.SaveChangesAsync();

             return new GetTrainDTO()
            {
                Capacity = entity.Capacity,
                TrainId = entity.VehicleId,
                TransportationProviderId = entity.TransportationProviderId,
                TransportationpProviderName = TransportationProvider.Name
            }; 
        }

        public async Task<GetTrainDTO> GetById(object Id)
        {
            var entity = await unitOfWork.Vehicles.GetByIdAsync(Id);

            if (entity == null)
            {
                return null;
            }

            return new GetTrainDTO
            {
                Capacity = entity.Capacity,
                TrainId = entity.VehicleId,
                TransportationProviderId = entity.TransportationProviderId
            };
        }
    }
}
