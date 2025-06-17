using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly TripValidator tripValidator;

        public TripService(IUnitOfWork unitOfWork, TripValidator tripValidator)
        {
            this.unitOfWork = unitOfWork;
            this.tripValidator = tripValidator;
        }

        public async Task<Trip> GetTrip(Guid id, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.Trips.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Guid> CreateTrip(CreateTripDTO dto)
        {
            var result = await tripValidator.ValidateCreateTripDTO(dto);

            if (!result.IsValid)
                throw new ApplicationError(result.ErrorMessage);

            var trip = new Trip()
            {
                StartCityId = dto.StartCityId,
                EndCityId = dto.EndCityId,
                TransportationProviderId = dto.TransortationProviderId,
                VehicleId = dto.VehicleId,
                TypeOfTransportation = result.TransportationType,
                StartTime = dto.StartTime.Value,
                EndTime = dto.StartTime.Value,
                AdultPrice = dto.AdultPrice.Value,
                ChildrenPrice = dto.ChildrenPrice.Value,
                Capacity = result.Vehicle.Capacity
            };

            await unitOfWork.Trips.AddAsync(trip);
            await unitOfWork.SaveChangesAsync();

            return trip.TripId;
        }

        public async Task<List<TripSearchResultDTO>> GetAllSearchResultTrips(SearchTripsDTO searchTripsDTO, CancellationToken cancellationToken)
        {
            //parse dates
            bool parseDepartureTime = DateOnly.TryParse(searchTripsDTO.StartDate, out DateOnly tripStartTime);
            bool parseArrvalTime = DateOnly.TryParse(searchTripsDTO.EndDate, out DateOnly tripEndTime);

            if (!parseArrvalTime || !parseDepartureTime)
            {
                throw new ApplicationError(ResponseConstants.InvalidDateFormat);
            }

            //prepare dates for filter function 

            DateTime DepartureStartTime = tripStartTime.ToDateTime(TimeOnly.MinValue);
            DateTime DepartureEndTime = tripStartTime.ToDateTime(TimeOnly.MaxValue);

            DateTime ArrivalStartTime = tripEndTime.ToDateTime(TimeOnly.MinValue);
            DateTime ArrivalEndTime = tripEndTime.ToDateTime(TimeOnly.MaxValue);

            //FOR TESTING PURPOSES -> RETURN ALL TRIPS SO WE HAVE MORE DATA TO WORK WITH
            var data = await unitOfWork.Trips.GetAllAsync(
               // x => x.StartCityId == searchTripsDTO.StartCityId &&
               //  x.EndCityId == searchTripsDTO.EndCityId &&
               // x.StartTime >= DepartureStartTime && x.StartTime <= DepartureEndTime &&
               // x.EndTime >= ArrivalStartTime && x.EndTime <= ArrivalEndTime,
               searchTripsDTO.Type != "all" ? x => x.TypeOfTransportation.ToString() == searchTripsDTO.Type : null,
               x => x.OrderByDescending(t => t.AdultPrice),
                true,
                cancellationToken,
               x => x.EndCity,
               x => x.StartCity,
               x => x.TransportationProvider
                );

            var result = data.Select(x => new TripSearchResultDTO
            {
                TripId = x.TripId,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                EndCityName = x.EndCity.CityName,
                AdultPrice = x.AdultPrice,
                ChildrenPrice = x.ChildrenPrice,
                StartCityName = x.StartCity.CityName,
                TransportationProviderName = x.TransportationProvider.Name,
                Currency = Enum.GetName(x.Currency),
                TransportationProviderLogo = x.TransportationProvider?.Logo != null ? Convert.ToBase64String(x.TransportationProvider?.Logo ) : string.Empty ,
                TypeOfTrip = Enum.GetName(x.TypeOfTransportation)
            }).ToList();

            return result;
        }

        /// <summary>
        /// Amends a trip. Only the following properties can be editet: AdultPrice, ChildrenPrice, VehicleId (change vehicle for the trip, but not the type), Start- and EndTime. For all other changes the trip must be canceled
        /// an a new one provided, as this will be a siginificant change to the entity and not optimal for users.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationError"></exception>
        public async Task<Trip> UpdateTrip(UpdateTripDTO dto)
        {
            var trip = await unitOfWork.Trips.GetByIdAsync(dto.TripId);

            if (trip is null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Trip), dto.TripId));
            }

            //VEHICLE CHANGE
            if (dto.VehicleId.HasValue && trip.VehicleId != dto.VehicleId.Value)
            {
                var oldVehicle = await unitOfWork.Vehicles.GetByIdAsync(trip.VehicleId);
                var newVehicle = await unitOfWork.Vehicles.GetByIdAsync(dto.VehicleId);

                if (newVehicle is null)
                {
                    throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Vehicle), dto.VehicleId));
                }

                bool ownershipChanged = oldVehicle.TransportationProviderId != newVehicle.TransportationProviderId;

                if (ownershipChanged)
                    throw new ApplicationError(ResponseConstants.OwnershipChangeNotAllowed);

                trip.Vehicle = newVehicle;
            }

            trip.StartTime = dto.StartTime ?? trip.StartTime;
            trip.EndTime = dto.EndTime ?? trip.EndTime;
            trip.AdultPrice = dto.AdultPrice ?? trip.AdultPrice;
            trip.ChildrenPrice = dto.ChildrenPrice ?? trip.ChildrenPrice;

            unitOfWork.Trips.Update(trip);
            await unitOfWork.SaveChangesAsync();

            return trip;
        }
    }
}
