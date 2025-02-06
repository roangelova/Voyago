using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.DTOs.User;
using GetMyTicket.Common.Entities;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace GetMyTicket.Service.Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork unitOfWork;

        public TripService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Trip>> GetAllSearchResultTrips(SearchTripsDTO searchTripsDTO)
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

            var data = await unitOfWork.Trips.GetAllAsync(
                x => x.StartCityId == searchTripsDTO.StartCityId &&
                x.EndCityId == searchTripsDTO.EndCityId &&
                x.StartTime >= DepartureStartTime && x.StartTime <= DepartureEndTime &&
                x.EndTime >= ArrivalStartTime && x.EndTime <= ArrivalEndTime,
                x => x.OrderByDescending(t => t.Price),
                true
                );

            return data;
        }
    }
}
