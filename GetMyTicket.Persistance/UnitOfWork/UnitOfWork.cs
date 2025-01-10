using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Persistance.Context;
using GetMyTicket.Persistance.Generic_Repository;

namespace GetMyTicket.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public readonly AppDbContext AppDbContext;

        public IGenericRepository<TransportationProvider> TransportationProviders {  get; }

        public IGenericRepository<Passenger> Passengers { get; }

        public IGenericRepository<Vehicle> Vehicles { get; }

        public IGenericRepository<Booking> Bookings { get; }

        public IGenericRepository<Trip> Trips { get; }

        public IGenericRepository<BaggageItem> BaggageItems { get; }

        public IGenericRepository<City> Cities { get; }

        public IGenericRepository<Country> Countries { get; }

        public IGenericRepository<User> Users { get; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;

            TransportationProviders = new GenericRepository<TransportationProvider>(AppDbContext);
            Passengers = new GenericRepository<Passenger>(AppDbContext);
            Vehicles = new GenericRepository<Vehicle>(AppDbContext);
            Bookings = new GenericRepository<Booking>(AppDbContext);
            Trips = new GenericRepository<Trip>(AppDbContext);
            Countries = new GenericRepository<Country>(AppDbContext);
            Cities = new GenericRepository<City>(AppDbContext);
            Users = new GenericRepository<User>(AppDbContext);
            BaggageItems = new GenericRepository<BaggageItem>(AppDbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await AppDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            AppDbContext.Dispose();
        }
    }
}
