using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Passengers;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Common.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetMyTicket.Persistance.Context
{
    public class AppDbContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TransportationProvider> TransportationProviders { get; set; }

        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<Bus> Buses { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Trip> Tips { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Adult> Adults { get; set; }

        public DbSet<Child> Children { get; set; }

        public DbSet<Infant> Infants { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Trip>()
                .HasOne(x => x.Vehicle)
                .WithMany(y => y.Trips)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Trip>()
                .Property(x => x.Currency)
                .HasDefaultValue(Currency.EUR)
                .HasSentinel(Currency.BGN);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSeeding((Context, _) =>
            {
                var Germany = new Country
                {
                    CountryId = Guid.CreateVersion7(),
                    Name = "Germany",
                    Destinations = new List<City>()
                     {
                         new City
                         {
                             CityId = Guid.CreateVersion7(),
                             CityName = "Munich",
                             IATA_Code = "MUC"
                         }
                     }
                };

                var Bulgaria = new Country
                {
                    CountryId = Guid.CreateVersion7(),
                    Name = "Bulgaria",
                    Destinations = new List<City>()
                     {
                         new City
                         {
                             CityId = Guid.CreateVersion7(),
                             CityName = "Varna",
                             IATA_Code = "VAR"
                         }
                     }
                };

                var TransportationProvider = new TransportationProvider
                {
                    TransportationProviderId = Guid.CreateVersion7(),
                    Name = "TransAvia",
                    Address = "Germany, 81539 Munich, Bayerstr. 18",
                    Email = "transavia_management@gmail.com",
                    Description = "TransAvis is a leading airline in Europe, connection more than 400 destinations in 15 countries."
                };

                var airplane1 = new Airplane
                {
                    VehicleId = Guid.CreateVersion7(),
                    TransportationProvideriD = TransportationProvider.TransportationProviderId,
                    AirplaneManufacturer = Common.Enum.AirplaneManufacturer.Airbus,
                    Model = "A-320",
                    Capacity = 180,
                    ManufacturingDate = new DateOnly(10, 10, 2022)
                };

                var OurFirstTrip = new Trip
                {
                    TransportationProviderId = TransportationProvider.TransportationProviderId,
                    VehicleId = airplane1.VehicleId,
                    StartTime = new DateTime(18, 05, 2025, 18, 00, 00),
                    EndTime = new DateTime(18, 05, 2025, 20, 20, 00),
                    StartCity = Germany.Destinations.First(),
                    EndCity = Bulgaria.Destinations.First(),
                    Price = 220
                };

            });
        }
    }
}
