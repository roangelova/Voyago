using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IUnitOfWork unitOfWork;

        public AirplaneService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Airplane> Add(CreateAirplaneDTO airplaneDTO)
        {
            if (Enum.TryParse<AirplaneManufacturer>(airplaneDTO.Manufacturer,
                    out AirplaneManufacturer manufacturer))
            {

                if (airplaneDTO.Capacity <= 0)
                {
                    throw new ApplicationError(string.Format(ErrorMessages.CantBeBull, nameof(airplaneDTO.Capacity)));
                }

                if (string.IsNullOrWhiteSpace(airplaneDTO.Model))
                {
                    throw new ApplicationError(string.Format(ErrorMessages.NotSupported, nameof(airplaneDTO.Model)));
                }

                var entity = new Airplane()
                {
                    VehicleId = Guid.CreateVersion7(),
                    TransportationProvideriD = airplaneDTO.TpProviderId,
                    Model = airplaneDTO.Model.Trim(),
                    Capacity = airplaneDTO.Capacity,
                    ManufacturingDate = airplaneDTO.ManufacturingDate,
                    AirplaneManufacturer = manufacturer
                };

                await unitOfWork.Vehicles.AddAsync(entity);
                await unitOfWork.SaveChangesAsync();

                return entity;
            }

            throw new ApplicationError(string.Format(ErrorMessages.NotSupported, nameof(airplaneDTO.Manufacturer)));
        }

        public async Task<GetAirplaneDTO> GetById(object id)
        {
            Airplane entity = (Airplane)await unitOfWork.Vehicles.GetByIdAsync(id);

            if (entity != null)
            {
                return new GetAirplaneDTO(
                    entity.TransportationProvideriD,
                    Enum.GetName(entity.AirplaneManufacturer),
                    entity.Model,
                    entity.Capacity
                    );
            }

            return null;
        }
    }
}
