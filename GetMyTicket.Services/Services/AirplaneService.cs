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
        private readonly IUnitOfWork UnitOfWork;

        public AirplaneService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public async Task<Airplane> Add(CreateAirplaneDTO airplaneDTO)
        {
            if (Enum.TryParse<AirplaneManufacturer>(airplaneDTO.Manufacturer,
                    out AirplaneManufacturer Manufacturer))
            {

                if (airplaneDTO.Capacity <= 0)
                {
                    throw new ApplicationError(string.Format(ResponseConstants.CantBeNull, nameof(airplaneDTO.Capacity)));
                }

                if (string.IsNullOrWhiteSpace(airplaneDTO.Model))
                {
                    throw new ApplicationError(string.Format(ResponseConstants.NotSupported, nameof(airplaneDTO.Model)));
                }

                var Entity = new Airplane()
                {
                    Id = Guid.CreateVersion7(),
                    TransportationProviderId = airplaneDTO.TpProviderId,
                    Model = airplaneDTO.Model.Trim(),
                    Capacity = airplaneDTO.Capacity,
                    ManufacturingDate = airplaneDTO.ManufacturingDate,
                    AirplaneManufacturer = Manufacturer
                };

                await UnitOfWork.Vehicles.AddAsync(Entity);
                await UnitOfWork.SaveChangesAsync();

                return Entity;
            }

            throw new ApplicationError(string.Format(ResponseConstants.NotSupported, nameof(airplaneDTO.Manufacturer)));
        }

        public async Task<GetAirplaneDTO> GetById(object id)
        {
            Airplane Entity = (Airplane)await UnitOfWork.Vehicles.GetByIdAsync(id);

            if (Entity != null)
            {
                return new GetAirplaneDTO(
                    Entity.TransportationProviderId,
                    Enum.GetName(Entity.AirplaneManufacturer),
                    Entity.Model,
                    Entity.Capacity
                    );
            }

            return null;
        }
    }
}
