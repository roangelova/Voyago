using System.Text.Json;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Passengers;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Common.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GetMyTicket.Persistance.Context
{
    public class AppDbContext : IdentityDbContext<User, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext()
        {

        }

        public DbSet<TransportationProvider> TransportationProviders { get; set; }

        public DbSet<Airplane> Airplanes { get; set; }

        public DbSet<Bus> Buses { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Trip> Trips { get; set; }

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

            builder.Entity<Trip>()
                .HasOne(x => x.StartCity)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Trip>()
                .HasOne(x => x.EndCity)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSeeding((context, _) =>
              {
                  string seedFilePath = GetSeedDataPath();

                  Task.Run(async () => await SeedDataFromJSON(context, seedFilePath)).GetAwaiter().GetResult();
                  SeedData(context);
              });
        }

        private static void SeedData(DbContext context)
        {
            if (context.Set<TransportationProvider>().Any() &&
                   context.Set<Vehicle>().Any() &&
                    context.Set<Trip>().Any() &&
                    context.Set<User>().Any()
                    )
            {
                return;
            }

            var TransportationProvider = new TransportationProvider
            {
                TransportationProviderId = Guid.CreateVersion7(),
                Name = "TransAvia",
                Address = "Germany, 81539 Munich, Bayerstr. 18",
                Email = "transavia@gmail.com",
                Description = "TransAvis is a leading airline in Europe, connection more than 400 destinations in 15 countries."
            };

            var airplane1 = new Airplane
            {
                VehicleId = Guid.CreateVersion7(),
                TransportationProvideriD = TransportationProvider.TransportationProviderId,
                AirplaneManufacturer = Common.Enum.AirplaneManufacturer.Airbus,
                Model = "A-320",
                Capacity = 180,
                ManufacturingDate = new DateOnly(2022, 10, 10)
            };

            var OurFirstTrip = new Trip
            {
                TransportationProviderId = TransportationProvider.TransportationProviderId,
                VehicleId = airplane1.VehicleId,
                StartTime = new DateTime(2025, 5, 18, 18, 00, 00),
                EndTime = new DateTime(2025, 5, 18, 20, 20, 00),
                StartCityId = Guid.Parse("0195604c-c607-7d2f-8499-5139550bed23"),
                EndCityId = Guid.Parse("0195604c-c607-799a-b23c-038c1bd24f08"),
                Price = 220,
                Capacity = airplane1.Capacity
            };

            var User = new User
            {
                Id = Guid.CreateVersion7(),
                Email = "test@test.com",
                NormalizedEmail = "TEST@TEST.COM",
                FirstName = "Test",
                LastName = "Testov",
                DOB = new DateOnly(2000, 10, 10),
                UserName = "TestGuy",
                NormalizedUserName = "TESTGUY",
                Address = "Varna 9000 BG"
            };

            var passwordHasher = new PasswordHasher<User>();
            var hashed = passwordHasher.HashPassword(User, "Passw1rd#");

            User.PasswordHash = hashed;

            context.Set<User>().Add(User);
            context.Set<TransportationProvider>().Add(TransportationProvider);
            context.Set<Vehicle>().Add(airplane1);
            context.Set<Trip>().Add(OurFirstTrip);

        }

        /// <summary>
        /// Reads data from JSON and seeds some contries with the respective cities; Some have predifined Ids, which are then used to seed a trip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task SeedDataAsync(AppDbContext context)
        {
            string seedFilePath = GetSeedDataPath();
            await SeedDataFromJSON(context, seedFilePath);

            SeedData(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedDataFromJSON(DbContext context, string path)
        {

            if (context.Set<Country>().Any() && context.Set<City>().Any())
            {
                return;
            }

            var jsonData = await File.ReadAllTextAsync(path);

            var countries = JsonSerializer.Deserialize<List<Country>>(jsonData);

            await context.Set<Country>().AddRangeAsync(countries);
        }

        /// <summary>
        /// Returns the path containing the DestinationsSeed.json
        /// </summary>
        /// <returns>string path</returns>
        private static string GetSeedDataPath()
        {
            string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            string seedFilePath = Path.Combine(projectRoot, "GetMyTicket.Persistance", "SeedData", "DestinationsSeed.json");

            return seedFilePath;
        }
    }
}
