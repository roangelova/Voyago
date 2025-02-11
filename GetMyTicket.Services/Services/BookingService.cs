using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.Entities;
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

        public async Task<Guid> BookTrip(BookTripDTO bookTripDTO)
        {
            //TODO -> EX[AND TO WORK WITH MULTIPLE PASSENGERS PER BOOKING
            var passengerId = await passengerService.GetPassengerIdAsync(bookTripDTO.UserId);

            if(passengerId == Guid.Empty)
            {
                passengerId = await passengerService.CreatePassengerData(bookTripDTO);
            }

            var trip = await unitOfWork.Trips.GetByIdAsync(bookTripDTO.TripId);

            if (trip is null)
            {
                throw new ArgumentException(string.Format(ErrorMessages.NotFoundError, nameof(Trip), bookTripDTO.TripId));
            }

            //TODO-> ONCE 1+ PASSENGERS ARE SUPPORTED, CALCULATE TOTAL PRICE
            var booking = new Booking()
            {
                BookingId = Guid.CreateVersion7(),
                TotalPrice = trip.Price,
                BookingStatus = Common.Enum.BookingStatus.Confirmed
            };

            trip.Capacity--;

            var passengerBookingMap = new PassengerBookingMap
            {
                BookingId = booking.BookingId,
                PassengerId = passengerId,
            };

            //TODO-> review THIS again TO MAKE SURE U are not missing something and no partial changes will be persisted to the db
            unitOfWork.Trips.Update(trip);
            await unitOfWork.Bookings.AddAsync(booking);
            await unitOfWork.PassengerBookingMap.AddAsync(passengerBookingMap);

            await unitOfWork.SaveChangesAsync();

            //return booking Id 
            return booking.BookingId;

        }
    }
}
