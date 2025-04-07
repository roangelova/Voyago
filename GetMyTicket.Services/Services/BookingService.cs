using GetMyTicket.Common.Constants;
using GetMyTicket.Common.DTOs;
using GetMyTicket.Common.Entities;
using GetMyTicket.Common.ErrorHandling;
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

        public async Task<Guid> CreateBooking(CreateBookingDTO bookTripDTO)
        {
            //TODO -> EXTAND TO WORK WITH MULTIPLE PASSENGERS PER BOOKING
            //FOR NOW, WE USE A DEFAULT PASSENGERID, AS AT THIS STAGE THE REQUIRMENT FOR WHEN AND HOW THE PASSENGER DATA WILL BE CREATED IS UNCLEAR; 
           // var passengerId = await passengerService.GetPassengerIdAsync(bookTripDTO.UserId);

            var trip = await unitOfWork.Trips.GetByIdAsync(bookTripDTO.TripId);

            if (trip is null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Trip), bookTripDTO.TripId));
            }

            if(trip.Capacity == 0)
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

           // var passengerBookingMap = new PassengerBookingMap
           // {
           //     BookingId = booking.BookingId,
           //     PassengerId = passengerId,
           // };

            //TODO-> review THIS again TO MAKE SURE U are not missing something and no partial changes will be persisted to the db
            unitOfWork.Trips.Update(trip);
            await unitOfWork.Bookings.AddAsync(booking);
            //await unitOfWork.PassengerBookingMap.AddAsync(passengerBookingMap);

            await unitOfWork.SaveChangesAsync();
           
            return booking.BookingId;
        }
    }
}
