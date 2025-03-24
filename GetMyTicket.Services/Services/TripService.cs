using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork unitOfWork;

        public TripService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Trip> GetTrip(Guid id)
        {
           return await unitOfWork.Trips.GetByIdAsync(id);
        }

        public async Task<Guid> CreateTrip(CreateTripDTO dto)
        {
            var vehicle = await unitOfWork.Vehicles.GetByIdAsync(dto.VehicleId);
            var provider = await unitOfWork.TransportationProviders.GetByIdAsync(dto.TransortationProviderId);

            if(vehicle.TransportationProvideriD != provider.TransportationProviderId)
            {
                throw new ApplicationError(ErrorMessages.OwnershipMissmatch);
            }

            if(dto.StartCityId == dto.EndCityId)
            {
                throw new ApplicationError(string.Format(ErrorMessages.CantBeTheSame, nameof(dto.StartCityId), nameof(dto.EndCityId)));
            }

            bool parseResultTransportationType = Enum.TryParse<TypeOfTransportation>(dto.TypeOfTransportation, false, out var TransportationType);
            bool parseResultStartTime = DateTime.TryParse(dto.StartTime, out DateTime StartTime);
            bool parseResultEndTime = DateTime.TryParse(dto.EndTime, out DateTime EndTime);

            if (!parseResultTransportationType)
            {
                throw new ApplicationError(string.Format(ErrorMessages.InvalidType, nameof(dto.TypeOfTransportation), nameof(TransportationProvider)));
            }

            if (!parseResultStartTime || !parseResultEndTime) 
            {
                throw new ApplicationError(ErrorMessages.InvalidDateFormat);
            }

            var trip = new Trip()
            {
                StartCityId = dto.StartCityId,
                EndCityId = dto.EndCityId,
                TransportationProviderId = dto.TransortationProviderId,
                VehicleId = dto.VehicleId,
                TypeOfTransportation = TransportationType,
                StartTime = StartTime,
                EndTime = EndTime,
                Price = dto.Price,
                Capacity = vehicle.Capacity
            };

            await unitOfWork.Trips.AddAsync(trip);
            await unitOfWork.SaveChangesAsync();

            return trip.TripId;
        }

        public async Task<List<TripSearchResultDTO>> GetAllSearchResultTrips(SearchTripsDTO searchTripsDTO)
        {
            //parse dates
            bool parseDepartureTime = DateOnly.TryParse(searchTripsDTO.StartDate, out DateOnly tripStartTime);
            bool parseArrvalTime = DateOnly.TryParse(searchTripsDTO.EndDate, out DateOnly tripEndTime);

            if (!parseArrvalTime || !parseDepartureTime)
            {
                throw new ApplicationError(ErrorMessages.InvalidDateFormat);
            }

            //prepare dates for filter function 

            DateTime DepartureStartTime = tripStartTime.ToDateTime(TimeOnly.MinValue);
            DateTime DepartureEndTime = tripStartTime.ToDateTime(TimeOnly.MaxValue);

            DateTime ArrivalStartTime = tripEndTime.ToDateTime(TimeOnly.MinValue);
            DateTime ArrivalEndTime = tripEndTime.ToDateTime(TimeOnly.MaxValue);

            //FOR TESTING PURPOSES -> RETURN ALL TRIPS SO WE HAVE MORE DATA TO WORK WITH
            //TODO -> AZURESQL DB, SEED MORE DATA & PERSIST ALL ADDDATA ENTITIES
            var data = await unitOfWork.Trips.GetAllAsync(
               // x => x.StartCityId == searchTripsDTO.StartCityId &&
               //  x.EndCityId == searchTripsDTO.EndCityId &&
               // x.StartTime >= DepartureStartTime && x.StartTime <= DepartureEndTime &&
               // x.EndTime >= ArrivalStartTime && x.EndTime <= ArrivalEndTime,
               searchTripsDTO.Type != "all" ?  x => x.TypeOfTransportation.ToString() ==  searchTripsDTO.Type : null,
               x => x.OrderByDescending(t => t.Price) ,
                true,
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
                Price = x.Price,
                StartCityName = x.StartCity.CityName,
                TransportationProviderName = x.TransportationProvider.Name,
                Currency =Enum.GetName(x.Currency),
                TransportationProviderLogo = Convert.ToBase64String(x.TransportationProvider.Logo)
            }).ToList();

            return result;
        }
    }
}
