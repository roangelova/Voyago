using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.DTOs.Vehicle;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Common.Enum;
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

        public async Task<Airplane> Add(AddAirplaneDTO airplaneDTO)
        {
            try
            {
                if (Enum.TryParse<AirplaneManufacturer>(airplaneDTO.Manufacturer, 
                        out AirplaneManufacturer manufacturer))
                {

                    if(airplaneDTO.Capacity <= 0)
                    {
                        throw new ArgumentOutOfRangeException("Capacity can't be null");
                    }

                    if (string.IsNullOrWhiteSpace(airplaneDTO.Model))
                    {
                        throw new ArgumentException("Invalid aircraft model");
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

                throw new ArgumentException("Manufacturer not supported.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetAirplaneDTO> GetById(object id)
        {
            try
            {
                Airplane entity = (Airplane) await unitOfWork.Vehicles.GetByIdAsync(id);

                if(entity  != null)
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
