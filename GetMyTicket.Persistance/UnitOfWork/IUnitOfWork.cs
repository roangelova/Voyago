using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Mapping_Tables;
using GetMyTicket.Persistance.Generic_Repository;
using GetMyTicket.Persistance.Repositories.Contracts;

namespace GetMyTicket.Persistance.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<TransportationProvider> TransportationProviders { get; }
        IGenericRepository<Passenger> Passengers { get; }
        IGenericRepository<Vehicle> Vehicles { get; }
        IGenericRepository<Booking> Bookings { get; }
        ITripRepository Trips { get; }
        IGenericRepository<BaggageItem> BaggageItems { get; }
        IGenericRepository<BaggagePrice> BaggagePrices { get; }
        IGenericRepository<City> Cities { get; }
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<User> Users { get; }

        IGenericRepository<PassengerBookingMap> PassengerBookingMap {  get; } 
        IGenericRepository<UserPassengerMap> UserPassengerMap {  get; } 

        IGenericRepository<Discount> Discounts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
