using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;

namespace GetMyTicket.Service.Contracts
{
    public interface ITripService
    {
        public Task<Trip> GetTrip(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method retrieves all trips, as specified by the search form in the frontend. It returns all trips that match the criteria and then orders the trips by price in descending order; Data is returned AsNonTracking();
        /// </summary>
        /// <param name="searchTripsDTO">The data to filter the trips by</param>
        /// <returns>List of trips that match the search criteria</returns>
        public Task<List<TripSearchResultDTO>> GetAllSearchResultTrips(SearchTripsDTO data, CancellationToken cancellationToken = default);

        public Task<Guid> CreateTrip (CreateTripDTO dto);

        public Task<Trip> UpdateTrip(UpdateTripDTO dto);
    }
}
