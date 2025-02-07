using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;
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

        public async Task<List<TripSearchResultDTO>> GetAllSearchResultTrips(SearchTripsDTO searchTripsDTO)
        {
            //parse dates
            bool parseDepartureTime = DateOnly.TryParse(searchTripsDTO.StartDate, out DateOnly tripStartTime);
            bool parseArrvalTime = DateOnly.TryParse(searchTripsDTO.EndDate, out DateOnly tripEndTime);

            if (!parseArrvalTime || !parseDepartureTime)
            {
                throw new InvalidDataException("Incorrect trip time format.");
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
               null,
               x => x.OrderByDescending(t => t.Price),
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
                TransportationProviderName = x.TransportationProvider.Name 
            }).ToList();

            return result;
        }
    }
}
