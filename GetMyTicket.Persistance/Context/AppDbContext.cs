﻿using System.Text.Json;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.Mapping_Tables;
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

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Discount> Discounts { get; set; }


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

            builder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            builder.Entity<UserPassengerMap>()
                 .HasKey(up => new { up.UserId, up.PassengerId });

            builder.Entity<UserPassengerMap>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPassengerMaps)
                .HasForeignKey(up => up.UserId);

            builder.Entity<UserPassengerMap>()
                .HasOne(up => up.Passenger)
                .WithMany(p => p.UserPassengerMaps)
                .HasForeignKey(up => up.PassengerId);

            builder.Entity<PassengerBookingMap>()
                .HasKey(pb => new { pb.PassengerId, pb.BookingId });

            builder.Entity<PassengerBookingMap>()
                .HasOne(pb => pb.Passenger)
                .WithMany(p => p.PassengerBookingMaps)
                .HasForeignKey(pb => pb.PassengerId);

            builder.Entity<PassengerBookingMap>()
                .HasOne(pb => pb.Booking)
                .WithMany(b => b.PassengerBookingMap)
                .HasForeignKey(pb => pb.BookingId);

            builder.Entity<BaggagePrice>()
                .HasOne(x => x.TransportationProvider)
                .WithMany(x => x.BaggagePrices)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Booking>()
                .HasOne(x => x.Discount)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.DiscountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Discount>()
                .HasIndex(x => x.Name)
                .IsUnique();


            ApplyGlobalQueryFilters(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


            optionsBuilder.UseSeeding((context, _) =>
              {
                  string seedFilePath = GetSeedDataPath();

                  Task.Run(async () => await SeedDataFromJSON(context, seedFilePath)).GetAwaiter().GetResult();
                  SeedData(context);
                  SaveChanges();
              });
        }

        private static void SeedData(DbContext context)
        {
            if (context.Set<TransportationProvider>().Any() &&
                   context.Set<Vehicle>().Any() &&
                    context.Set<Trip>().Any() &&
                    context.Set<User>().Any() &&
                    context.Set<Country>().Any() &&
                    context.Set<City>().Any())
            {
                return;
            }

            Guid DeutscheBahnId = Guid.CreateVersion7();
            Guid TransAviaId = Guid.CreateVersion7();
            Guid FlixbusId = Guid.CreateVersion7();

            var TransAvia = new TransportationProvider
            {
                Id = Guid.CreateVersion7(),
                Name = "TransAvia",
                Address = "Germany, 81539 Munich, Bayerstr. 18",
                Email = "transavia@gmail.com",
                Description = "TransAvis is a leading airline in Europe, connection more than 400 destinations in 15 countries.",
                BaggagePrices = [
                    new() { BaggageSize = BaggageSize.CarryOn, Price = 0 },
                    new() { BaggageSize = BaggageSize.Small, Price = 40 },
                    new() { BaggageSize = BaggageSize.Large, Price = 60 }],
                Logo = GetLogoAsByteArray("transavia.jpg")
            };

            var DeutscheBahn = new TransportationProvider
            {
                Id = DeutscheBahnId,
                Name = "Deutsche Bahn",
                Address = "Germany, 8000 Moenchengladbach",
                Email = "db@bahn.de",
                Description = "The leading provider for train connections in Germany",
                BaggagePrices = [
                    new() { BaggageSize = BaggageSize.CarryOn, Price = 0 },
                    new() { BaggageSize = BaggageSize.Small, Price = 0 },
                    new() { BaggageSize = BaggageSize.Large, Price = 0 }],
                Logo = GetLogoAsByteArray("deutscheBahn.png")
            };

            var train = new Train
            {
                Id = Guid.CreateVersion7(),
                Capacity = 300,
                HasBistroOnBoard = true,
                IsAHighspeedTrain = true,
                TransportationProviderId = DeutscheBahnId
            };

            var trainTrip =
                   new Trip
                   {
                       TransportationProviderId = DeutscheBahnId,
                       VehicleId = train.Id,
                       StartTime = new DateTime(2026, 5, 20, 18, 00, 00),
                       EndTime = new DateTime(2026, 5, 21, 20, 20, 00),
                       StartCityId = Guid.Parse("0195604c-c607-7d2f-8499-5139550bed23"),
                       EndCityId = Guid.Parse("0195604c-c607-799a-b23c-038c1bd24f08"),
                       AdultPrice = 130,
                       ChildrenPrice = 30,
                       Capacity = train.Capacity
                   };

            var airplane1 = new Airplane
            {
                Id = Guid.CreateVersion7(),
                TransportationProviderId = TransAviaId,
                AirplaneManufacturer = Common.Enum.AirplaneManufacturer.Airbus,
                Model = "A-320",
                Capacity = 180,
                ManufacturingDate = new DateOnly(2022, 10, 10)
            };

            var flight = new Trip
            {
                TransportationProviderId = TransAviaId,
                VehicleId = airplane1.Id,
                StartTime = new DateTime(2026, 5, 18, 18, 00, 00),
                EndTime = new DateTime(2026, 5, 18, 20, 20, 00),
                StartCityId = Guid.Parse("0195604c-c607-7d2f-8499-5139550bed23"),
                EndCityId = Guid.Parse("0195604c-c607-799a-b23c-038c1bd24f08"),
                AdultPrice = 220,
                ChildrenPrice = 100,
                Capacity = airplane1.Capacity
            };

            var FlixBus = new TransportationProvider
            {
                Id = FlixbusId,
                Name = "FlixBus",
                Address = "Germany, 81539 Munich, Bayerstr. 18",
                Email = "flixbus@office.com",
                Description = "Flixus connects you to every country in Europe",
                BaggagePrices = [
                    new() { BaggageSize = BaggageSize.CarryOn, Price = 0 },
                    new() { BaggageSize = BaggageSize.Small, Price = 0 },
                    new() { BaggageSize = BaggageSize.Large, Price = 0 }],
                Logo = GetLogoAsByteArray("flixbus.png")
            };

            var bus = new Bus
            {
                Id = Guid.CreateVersion7(),
                HasToiletOnBoard = true,
                Capacity = 55,
                TransportationProviderId = FlixbusId
            };

            var busTrip = new Trip
            {
                TransportationProviderId = FlixbusId,
                VehicleId = bus.Id,
                StartTime = new DateTime(2026, 5, 19, 18, 00, 00),
                EndTime = new DateTime(2026, 5, 21, 07, 30, 00),
                StartCityId = Guid.Parse("0195604c-c607-7d2f-8499-5139550bed23"),
                EndCityId = Guid.Parse("0195604c-c607-799a-b23c-038c1bd24f08"),
                AdultPrice = 150,
                ChildrenPrice = 50,
                Capacity = bus.Capacity
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
            context.Set<TransportationProvider>().AddRange(TransAvia, DeutscheBahn, FlixBus);
            context.Set<Vehicle>().AddRange(airplane1, train, bus);
            context.Set<Trip>().AddRange(flight, trainTrip, busTrip);
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

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<ITrackableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                    entry.Entity.IsActive = false;
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.IsActive = true;
                    entry.Entity.IsDeleted = false;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries<ITrackableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        /// <summary>
        /// This method applies a global filter to out entities, to make sure that soft-deleted objects are not retrieved 
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            //TODO -> should a QF also be applied to the IsActive prop? Currently, we do not use it annywhere in our app, so we ignore it at this point
            modelBuilder.Entity<Passenger>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Vehicle>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Booking>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<City>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Country>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<TransportationProvider>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Trip>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<User>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ApplicationRole>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<BaggageItem>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<BaggagePrice>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<PassengerBookingMap>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<UserPassengerMap>().HasQueryFilter(x => x.IsDeleted == false);

        }

        private static byte[] GetLogoAsByteArray(string fileName)
        {
            var logoPath = Path.Combine(AppContext.BaseDirectory, "SeedData", "Logos", fileName);
            var logo = File.ReadAllBytes(logoPath);
            return logo;
        }
    }
}
