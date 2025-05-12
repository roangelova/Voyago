using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs.Trip;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Entities.Contracts;
using GetMyTicket.Common.Entities.Vehicles;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Persistance.Generic_Repository;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Services;
using Moq;

namespace GetMyTicket.Tests.Services
{
    public class TripServiceTests
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<IGenericRepository<TransportationProvider>> transportationProviderRepoMock;
        private readonly Mock<IGenericRepository<Vehicle>> vehicleRepoMock;
        private readonly Mock<IGenericRepository<Trip>> tripRepoMock;

        public TripServiceTests()
        {
            tripRepoMock = new Mock<IGenericRepository<Trip>>();
            vehicleRepoMock = new Mock<IGenericRepository<Vehicle>>();
            transportationProviderRepoMock = new Mock<IGenericRepository<TransportationProvider>>();

            unitOfWork = new Mock<IUnitOfWork>();
        }

        //GET TRIP TESTS

        [Fact]
        public async Task GetTrip_WhenProvidedGuidMatchesExpectedTrip_ReturnsTrip()
        {
            var tripId = Guid.NewGuid();
            var expectedTrip = new Trip { TripId = tripId };

            tripRepoMock
                .Setup(repo => repo.GetByIdAsync(tripId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedTrip);

            unitOfWork.Setup(u => u.Trips).Returns(tripRepoMock.Object);
            var tripService = new TripService(unitOfWork.Object);

            var actualTrip = await tripService.GetTrip(tripId);

            Assert.Equal(expectedTrip, actualTrip);
        }

        //CREATE TRIPS TESTS

        [Fact]
        public async Task CreateTrip_WhenThereIsAVehicleAndTPProviderMissmatch_ThrowsApplicationError()
        {
            var provider = new TransportationProvider { TransportationProviderId = Guid.NewGuid() };

            //Vahicle in as abstract class
            var bus = new Bus
            {
                VehicleId = Guid.NewGuid(),
                TransportationProviderId = Guid.NewGuid()
            };

            vehicleRepoMock.Setup(repo => repo.GetByIdAsync(bus.VehicleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(bus);

            transportationProviderRepoMock.Setup(repo => repo.GetByIdAsync(provider.TransportationProviderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(provider);

            unitOfWork.Setup(u => u.Vehicles).Returns(vehicleRepoMock.Object);
            unitOfWork.Setup(u => u.TransportationProviders).Returns(transportationProviderRepoMock.Object);

            var tripService = new TripService(unitOfWork.Object);

            var dto = new CreateTripDTO
            {
                VehicleId = bus.VehicleId,
                TransortationProviderId = provider.TransportationProviderId,
            };

            var result = await Assert.ThrowsAsync<ApplicationError>(() => tripService.CreateTrip(dto));
            Assert.Equal(ResponseConstants.OwnershipMissmatch, result.Message);
        }

        [Fact]
        public async Task CreateTrip_WhenStartAndEndSitiIsTheSame_ThrowsApplicationError()
        {
            var provider = new TransportationProvider { TransportationProviderId = Guid.NewGuid() };

            var bus = new Bus
            {
                VehicleId = Guid.NewGuid(),
                TransportationProviderId = Guid.NewGuid()
            };

            vehicleRepoMock.Setup(repo => repo.GetByIdAsync(bus.VehicleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(bus);

            transportationProviderRepoMock.Setup(repo => repo.GetByIdAsync(provider.TransportationProviderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(provider);

            unitOfWork.Setup(u => u.Vehicles).Returns(vehicleRepoMock.Object);
            unitOfWork.Setup(u => u.TransportationProviders).Returns(transportationProviderRepoMock.Object);

            var tripService = new TripService(unitOfWork.Object);

            var City = new City { CityId = Guid.NewGuid() };
            var dto = new CreateTripDTO
            {
                StartCityId = City.CityId,
                EndCityId = City.CityId,
                VehicleId = bus.VehicleId,
                TransortationProviderId = provider.TransportationProviderId,
            };

            await Assert.ThrowsAsync<ApplicationError>(() => tripService.CreateTrip(dto));
        }

        [Fact]
        public async Task CreateTrip_InvalidTransportationType_ThrowsApplicationError()
        {
            var provider = new TransportationProvider { TransportationProviderId = Guid.NewGuid() };

            var bus = new Bus
            {
                VehicleId = Guid.NewGuid(),
                TransportationProviderId = Guid.NewGuid()
            };

            vehicleRepoMock.Setup(repo => repo.GetByIdAsync(bus.VehicleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(bus);

            transportationProviderRepoMock.Setup(repo => repo.GetByIdAsync(provider.TransportationProviderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(provider);

            unitOfWork.Setup(u => u.Vehicles).Returns(vehicleRepoMock.Object);
            unitOfWork.Setup(u => u.TransportationProviders).Returns(transportationProviderRepoMock.Object);

            var tripService = new TripService(unitOfWork.Object);

            var City = new City { CityId = Guid.NewGuid() };
            var dto = new CreateTripDTO
            {
                StartCityId = City.CityId,
                EndCityId = City.CityId,
                VehicleId = bus.VehicleId,
                TransortationProviderId = provider.TransportationProviderId,
                TypeOfTransportation = "Boat"
            };

            await Assert.ThrowsAsync<ApplicationError>(() => tripService.CreateTrip(dto));
        }
    }
}