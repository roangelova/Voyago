using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Persistance.UnitOfWork;

namespace GetMyTicket.Service
{
    public class TripValidator
    {
        private readonly IUnitOfWork unitOfWork;

        public TripValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TripValidationResult> ValidateCreateTripDTO(CreateTripDTO dto)
        {
            var vehicle = await unitOfWork.Vehicles.GetByIdAsync(dto.VehicleId);
            var provider = await unitOfWork.TransportationProviders.GetByIdAsync(dto.TransortationProviderId);

            if (vehicle.TransportationProviderId != provider.Id)
            {
                return new TripValidationResult { ErrorMessage = ResponseConstants.OwnershipMissmatch };
            }

            if (dto.EndTime <= dto.StartTime)
            {
                return new TripValidationResult
                {
                    ErrorMessage = ResponseConstants.InvalidTripTime
                };
            }

            //TODO -> ADD VALIDATION FOR WHEN THERE IS A MISSMATCH BETWEEN TRIPS TYPE AND VEHICLES TYPE. OR JUST DERIVE THE TRIP TYPE FROM THE VEHICLE TYPE

            if (dto.StartCityId == dto.EndCityId)
            {
                return new TripValidationResult
                {
                    ErrorMessage = string.Format(ResponseConstants.CantBeTheSame, nameof(dto.StartCityId), nameof(dto.EndCityId))
                };
            }

            if (!Enum.TryParse<TypeOfTransportation>(dto.TypeOfTransportation, false, out var transportationType))
            {
                return new TripValidationResult
                {
                    ErrorMessage = string.Format(ResponseConstants.InvalidType, nameof(dto.TypeOfTransportation), nameof(TransportationProvider))
                };
            }

            return new TripValidationResult
            {
                Vehicle = vehicle,
                Provider = provider,
                TransportationType = transportationType
            };
        }
    }
}
