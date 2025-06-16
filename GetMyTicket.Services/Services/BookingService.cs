using GetMyTicket.Common.Constants;
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
            var trip = await unitOfWork.Trips.GetByIdAsync(bookTripDTO.TripId);

            if (trip is null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Trip), bookTripDTO.TripId));
            }

            //TODO -> HOW DO WE WANT TO HANDLE CAPACITY AVAILABILITY CHECKS? BOTH CLIENT- AND SERVER-SIDE
            if (trip.Capacity == 0)
            {
                throw new ApplicationError(ResponseConstants.SoldOut);
            }

            var booking = new Booking()
            {
                BookingId = Guid.CreateVersion7(),
                TotalPrice = 0,
                BookingStatus = BookingStatus.Confirmed,
                TripId = bookTripDTO.TripId
            };

            foreach (var passenger in bookTripDTO.Passengers)
            {
                //NOTE: Infants travel free of charge. They do not occupy a seat. They do however need to be registered a passenger to travel and are
                //therefore mapped to a booking.
                switch (passenger.Type)
                {
                    case "Adult":
                        booking.TotalPrice += trip.AdultPrice;
                        trip.Capacity--;
                        break;
                    case "Child":
                        booking.TotalPrice += trip.ChildrenPrice;
                        trip.Capacity--;
                        break;
                }

                booking.PassengerBookingMap.Add(new PassengerBookingMap
                {
                    BookingId = booking.BookingId,
                    PassengerId = passenger.Id
                });
            }

            unitOfWork.Trips.Update(trip);
            await unitOfWork.Bookings.AddAsync(booking);

            await unitOfWork.SaveChangesAsync();

            return booking.BookingId;
        }

        //TODO -> HOW DO WE WANT TO LIST THE PASSENGERS IN THE BOOKINGS?
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
                x => x.Booking.Trip.StartCity,
                x => x.Booking.Trip.EndCity);

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
