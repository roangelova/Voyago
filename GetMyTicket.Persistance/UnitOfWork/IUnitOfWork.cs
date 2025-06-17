using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Mapping_Tables;
using GetMyTicket.Persistance.Generic_Repository;

namespace GetMyTicket.Persistance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<TransportationProvider> TransportationProviders { get; }
        IGenericRepository<Passenger> Passengers { get; }
        IGenericRepository<Vehicle> Vehicles { get; }
        IGenericRepository<Booking> Bookings { get; }
        IGenericRepository<Trip> Trips { get; }
        IGenericRepository<BaggageItem> BaggageItems { get; }
        IGenericRepository<City> Cities { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<User> Users { get; }

        IGenericRepository<PassengerBookingMap> PassengerBookingMap {  get; } 
        IGenericRepository<UserPassengerMap> UserPassengerMap {  get; } 

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
