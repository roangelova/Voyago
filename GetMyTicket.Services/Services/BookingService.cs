using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.DTOs.Booking;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.Enum;
using GetMyTicket.Common.ErrorHandling;
using GetMyTicket.Common.Mapping_Tables;
using GetMyTicket.Persistance.UnitOfWork;
using GetMyTicket.Service.Contracts;

namespace GetMyTicket.Service.Services
{
    public class BookingService : IBookingService
    {
        //DONT FORGET TO SUBSTRACT THE CAPACITY OF THE VEHICLE FOR THE TRIP AFTER MAKING THE BOOKING;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPassengerService passengerService;

        public BookingService(
            IUnitOfWork unitOfWork,
            IPassengerService passengerService)
        {
            this.unitOfWork = unitOfWork;
            this.passengerService = passengerService;
        }

        public async Task<int> CancelBooking(object bookingId)
        {
            var booking = await unitOfWork.Bookings.GetByIdAsync(bookingId);

            if (booking is null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Booking), bookingId));
            }

            booking.BookingStatus = BookingStatus.Canceled;
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<Guid> CreateBooking(CreateBookingDTO bookTripDTO)
        {
            //TODO -> EXTAND TO WORK WITH MULTIPLE PASSENGERS PER BOOKING

            var trip = await unitOfWork.Trips.GetByIdAsync(bookTripDTO.TripId);

            if (trip is null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Trip), bookTripDTO.TripId));
            }

            if (trip.Capacity == 0)
            {
                throw new ApplicationError(ResponseConstants.SoldOut);
            }

            //TODO-> ONCE 1+ PASSENGERS ARE SUPPORTED, CALCULATE TOTAL PRICE
            //TODO -> ONCE PASSENGERS IS IMPLEMENTED, ADD BAGGAGE OPTION; 
            var booking = new Booking()
            {
                BookingId = Guid.CreateVersion7(),
                TotalPrice = trip.Price,
                BookingStatus = Common.Enum.BookingStatus.Confirmed,
                TripId = bookTripDTO.TripId
            };

            trip.Capacity--;

            var passengerBookingMap = new PassengerBookingMap
            {
                BookingId = booking.BookingId,
                PassengerId = bookTripDTO.PassengerId,
            };

            unitOfWork.Trips.Update(trip);
            await unitOfWork.Bookings.AddAsync(booking);
            await unitOfWork.PassengerBookingMap.AddAsync(passengerBookingMap);

            await unitOfWork.SaveChangesAsync();

            return booking.BookingId;
        }

        public async Task<IEnumerable<ListBookingDTO>> GetUserBookings(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId);

            var data = await unitOfWork.PassengerBookingMap
                .GetAllAsync(x => user.PassengerMapId == x.PassengerId, 
                null,
                true,
                cancellationToken,
                //INCLUDE
                x => x.Booking,
                x=> x.Booking.Trip.StartCity,
                x=> x.Booking.Trip.EndCity);

            return data.Select(b => new ListBookingDTO()
            {
                BookingId = b.BookingId,
                ToCityName = b.Booking.Trip.EndCity.CityName,
                FromCityName = b.Booking.Trip.StartCity.CityName,
                DepartureTime = b.Booking.Trip.StartTime,
                TotalPrice = b.Booking.TotalPrice,
                Currency = Enum.GetName(b.Booking.Trip.Currency), 
                Status = Enum.GetName(b.Booking.BookingStatus),
                TripId = b.Booking.TripId
            });
        }
    }
}
