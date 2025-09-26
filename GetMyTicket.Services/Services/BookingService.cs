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
                //booking price is initially set to 0 and then we start adding services based on the passengers on the bookig, their booked baggage,any discount applied etc; 
                Id = Guid.CreateVersion7(),
                TotalPrice = 0,
                BookingStatus = BookingStatus.Confirmed,
                TripId = bookTripDTO.TripId,
                DiscountId = bookTripDTO.DiscountId, //each booking keeps a record of the discount used 
            };

            foreach (var passengerId in bookTripDTO.PassengerIds)
            {
                var passenger = await unitOfWork.Passengers.GetByIdAsync(passengerId);

                if (passenger is null)
                {
                    throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(Passenger), passengerId));
                }

                //NOTE: Infants travel free of charge. They do not occupy a seat. They do however need to be registered a passenger to travel and are
                //therefore mapped to a booking.
                switch (passenger.PassengerType)
                {
                    case PassengerType.Adult:
                        booking.TotalPrice += trip.AdultPrice;
                        trip.Capacity--;
                        break;
                    case PassengerType.Child:
                        booking.TotalPrice += trip.ChildrenPrice;
                        trip.Capacity--;
                        break;
                }

                booking.PassengerBookingMap.Add(new PassengerBookingMap
                {
                    BookingId = booking.Id,
                    PassengerId = passengerId
                });

                if (bookTripDTO.Baggage.Count > 0)
                {
                    var baggagePricesForProvider = await unitOfWork.BaggagePrices.GetAllAsync(x => x.TransportationProviderId == trip.TransportationProviderId);
                    if(baggagePricesForProvider.Count() < Enum.GetValues<BaggageSize>().Length)
                    {
                        //missing or incorrect baggage prices for provider
                        throw new ApplicationError(ResponseConstants.SomethingWentWrong);
                    }

                    foreach (var bg in bookTripDTO.Baggage)
                    {
                        for (int i = 0; i < bg.Amount; i++)
                        {
                            booking.BaggageItems.Add(new BaggageItem
                            {
                                Id = Guid.CreateVersion7(),
                                Passenger = passenger,
                                Size = Enum.Parse<BaggageSize>(bg.Type),
                                Trip = trip
                            });
                            booking.TotalPrice += baggagePricesForProvider.Where(x => Enum.GetName<BaggageSize>(x.BaggageSize) == bg.Type).First().Price;
                        }
                    }
                }

                //TODO -> CAN WE REFACTOR ALL OF THIS? 
            }

            //Discount is applied to the total price 
            //TODO -> DISCOUNT SHOULD NOT BE MORE THANT THE TOTAL PRICE! How do we want to check this?
            if (bookTripDTO?.DiscountId is not null)
            {
                var discount = await unitOfWork.Discounts.GetByIdAsync(bookTripDTO.DiscountId);

                if (Enum.GetName<DiscountType>(discount.DiscountType) == Enum.GetName<DiscountType>(DiscountType.Fixed))
                {
                    //apply if DiscountType is Fixed
                    booking.TotalPrice -= discount.Value;
                    booking.TotalDiscountUsed = discount.Value;
                }
                else
                {
                    //apply if DiscountType is Percent
                    var discountAmount = booking.TotalPrice * (discount.Value / 100);
                    booking.TotalPrice -= discountAmount;
                    booking.TotalDiscountUsed = discountAmount;
                }
            }

            await unitOfWork.Bookings.AddAsync(booking);

            await unitOfWork.SaveChangesAsync();

            return booking.Id;
        }


        public async Task<IEnumerable<ListBookingDTO>> GetUserBookings(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);

            if (user is null)
            {
                throw new ApplicationError(string.Format(ResponseConstants.NotFoundError, nameof(User), userId));
            }

            var passengerIds = await unitOfWork.UserPassengerMap.GetAllAsync(x => x.UserId == userId & x.IsAccountOwner == true);
            var passengerId = passengerIds.FirstOrDefault()?.PassengerId;

            if (passengerId is null)
            {
                // throw new ApplicationError(ResponseConstants.NoBookingsForThisUser);
                //TODO: User does not have passenger data yet. Return empty bookings for now  until we decide how we wantg to handle this
                return new List<ListBookingDTO>();
            }

            var data = await unitOfWork.PassengerBookingMap
                 .GetAllAsync(x => x.PassengerId == passengerId,
                 null,
                 true,
                 cancellationToken,
                 //INCLUDE
                 x => x.Booking,
                 x => x.Booking.Trip.StartCity,
                 x => x.Booking.Trip.EndCity);

            return data.Where(x => x.Booking.Trip.StartTime > DateTime.UtcNow).
                Select(b => new ListBookingDTO()
                {
                    BookingId = b.BookingId,
                    ToCityName = b.Booking.Trip.EndCity.CityName,
                    FromCityName = b.Booking.Trip.StartCity.CityName,
                    DepartureTime = b.Booking.Trip.StartTime,
                    TotalPrice = b.Booking.TotalPrice,
                    TotalDiscountUsed = b.Booking.TotalDiscountUsed,
                    Currency = Enum.GetName(b.Booking.Trip.Currency),
                    Status = Enum.GetName(b.Booking.BookingStatus),
                    TripId = b.Booking.TripId,
                    BookingDate = b.Booking.CreatedAt
                });
        }
    }

}
