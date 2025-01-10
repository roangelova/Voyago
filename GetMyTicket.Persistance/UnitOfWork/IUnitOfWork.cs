using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
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

        Task<int> SaveChangesAsync();
    }
}
